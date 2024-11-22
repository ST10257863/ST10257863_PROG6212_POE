using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using System.Text;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class ClaimsController : Controller
	{
		private readonly AppDbContext _context;

		public ClaimsController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Claims()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SubmitClaim(decimal hoursWorked, decimal overtimeWorked, string lecturerNotes, decimal hourlyRate, IList<IFormFile> documents) // Accepts a list of uploaded files
		{
			var lecturerId = HttpContext.Session.GetInt32("LecturerID");

			if (!lecturerId.HasValue)
			{
				ModelState.AddModelError("", "Lecturer ID is missing from the session.");
				return Json(new
				{
					success = false,
					message = "Lecturer ID is missing from the session."
				});
			}

			// If the hourly rate is not provided or invalid, retrieve it from the lecturer's account
			if (hourlyRate <= 0)
			{
				var lecturer = await _context.Lecturers.FirstOrDefaultAsync(l => l.LecturerID == lecturerId.Value);
				if (lecturer == null)
				{
					return Json(new
					{
						success = false,
						message = "Lecturer not found."
					});
				}

				hourlyRate = lecturer.HourlyRate; // Use the hourly rate from the lecturer's account

				if (hourlyRate <= 0)
				{
					return Json(new
					{
						success = false,
						message = "Lecturer's hourly rate is not set or invalid."
					});
				}
			}

			var claim = new Claim
			{
				LecturerId = lecturerId.Value,
				SubmissionDate = DateTime.Now,
				Status = "Pending",
				HourlyRate = hourlyRate // Store the hourly rate
			};

			// Validate Hours Worked
			if (hoursWorked > 0)
			{
				claim.HoursWorked = hoursWorked;
			}
			else
			{
				return Json(new
				{
					success = false,
					message = "Hours worked must be greater than zero."
				});
			}

			// Validate Overtime Worked
			if (overtimeWorked >= 0)
			{
				claim.OvertimeHoursWorked = overtimeWorked;
			}
			else
			{
				return Json(new
				{
					success = false,
					message = "Overtime worked must be zero or greater."
				});
			}

			// Validate Lecturer Notes
			if (!string.IsNullOrEmpty(lecturerNotes))
			{
				claim.LecturerNotes = lecturerNotes;
			}

			// Validate and process uploaded documents
			if (documents != null && documents.Count > 0)
			{
				foreach (var document in documents)
				{
					// Check file size (max 2MB)
					if (document.Length > 2 * 1024 * 1024) // 2MB
					{
						return Json(new
						{
							success = false,
							message = "File size must be less than 2MB."
						});
					}

					// Check file type (PDF, DOC, XLSX)
					var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".xlsx" };
					var fileExtension = Path.GetExtension(document.FileName).ToLower();
					if (!allowedExtensions.Contains(fileExtension))
					{
						return Json(new
						{
							success = false,
							message = "Only PDF, DOC, and XLSX files are allowed."
						});
					}

					// Process and save the document (you can store it or process as needed)
					// For example, saving the file to the server or a cloud storage service.
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", document.FileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await document.CopyToAsync(stream);
					}
				}
			}

			// Save the claim to the database
			_context.Claims.Add(claim);
			await _context.SaveChangesAsync();

			// After saving the claim, return the claimId in the response
			return Json(new
			{
				success = true,
				message = "Claim submitted successfully.",
				claimId = claim.ClaimId // Return the ClaimId for use in file upload
			});
		}

		// Fetches lecturer details based on session data.
		[HttpGet]
		public async Task<JsonResult> GetLecturerDetails()
		{
			var userId = HttpContext.Session.GetInt32("UserID");

			if (!userId.HasValue)
			{
				return Json(null);
			}

			var lecturer = await _context.Lecturers
				.Include(l => l.User)
				.FirstOrDefaultAsync(l => l.UserID == userId);

			if (lecturer == null)
			{
				return Json(null);
			}

			var lecturerDetails = new
			{
				LecturerId = lecturer.LecturerID,
				UserName = lecturer.User.UserName,
				FirstName = lecturer.User.FirstName,
				LastName = lecturer.User.LastName,
				HourlyRate = lecturer.HourlyRate,
				Department = lecturer.Department,
				Campus = lecturer.Campus
			};

			return Json(lecturerDetails);
		}

		// Retrieves all claims submitted by a lecturer.
		[HttpGet]
		public IActionResult LoadLecturerClaims()
		{
			var lecturerID = HttpContext.Session.GetInt32("LecturerID");

			var lecturerClaims = _context.Claims
				.Where(c => c.LecturerId == lecturerID)
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList();

			return Json(lecturerClaims);
		}

		// Calculates pay based on hours worked and overtime.
		[HttpPost]
		[Route("Claims/CalculatePay/{hoursWorked}/{overtimeWorked?}")]
		public JsonResult CalculatePay(double hoursWorked, double? overtimeWorked = null)
		{
			var lecturerID = HttpContext.Session.GetInt32("LecturerID");

			if (hoursWorked < 0 || (overtimeWorked.HasValue && overtimeWorked < 0))
			{
				return Json(new
				{
					error = "Invalid input values. Please enter valid hours."
				});
			}

			overtimeWorked ??= 0;

			var hourlyRate = _context.Lecturers
				.Where(l => l.LecturerID == lecturerID)
				.Select(l => l.HourlyRate)
				.FirstOrDefault();

			var regularPay = hoursWorked * (int)hourlyRate;
			var overtimePay = overtimeWorked.Value * (int)hourlyRate * 1.5; // .Value because overTimeWorked can be null
			var totalPay = regularPay + overtimePay;

			return Json(new
			{
				regularPay = regularPay,
				overtimePay = overtimePay,
				totalPay = totalPay
			});
		}

		[HttpGet]
		public IActionResult GetAllClaims()
		{
			var claims = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status,
					UserId = c.Lecturer.User.UserID // Assuming UserId is in the User model
				})
				.ToList();

			return Json(claims);
		}
		// Retrieves all claims with a "Pending" status and associated lecturer information.
		[HttpGet]
		public IActionResult GetAllPendingClaims()
		{
			var claims = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.Where(c => c.Status == "Pending")
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList();

			return Json(claims);
		}

		// Retrieves all claims with a "Verified" status and associated lecturer information.
		[HttpGet]
		public IActionResult GetAllVerifiedClaims()
		{
			var claims = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.Where(c => c.Status == "Verified")
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList();

			return Json(claims);
		}

		// Retrieves all claims with a "Approved" status and associated lecturer information.
		[HttpGet]
		public IActionResult GetAllApprovedClaims()
		{
			var claims = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.Where(c => c.Status == "Approved")
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList();

			return Json(claims);
		}

		[HttpGet]
		[Route("Claims/GetClaimDetails/{claimId}")]
		public IActionResult GetClaimDetails(int claimId)
		{
			var claim = _context.Claims
				.Include(c => c.Lecturer)
					.ThenInclude(l => l.User)  // Include associated User for the Lecturer
				.Include(c => c.Manager)  // Include AcademicManager for approval
				.Include(c => c.Coordinator)  // Include Coordinator for verification
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return NotFound("Claim not found.");
			}

			var claimDetails = new
			{
				// Lecturer and User details
				LecturerId = claim.Lecturer.LecturerID,
				FullName = $"{claim.Lecturer.User.FirstName} {claim.Lecturer.User.LastName}",
				HourlyRate = claim.HourlyRate, // Use the hourly rate from the claim
				Department = claim.Lecturer.Department,
				Campus = claim.Lecturer.Campus,

				// Claim details
				RegularHours = claim.HoursWorked,
				OvertimeHours = claim.OvertimeHoursWorked,
				TotalHours = claim.HoursWorked + claim.OvertimeHoursWorked,
				RegularPay = claim.HoursWorked * claim.HourlyRate, // Use the hourly rate from the claim
				OvertimePay = claim.OvertimeHoursWorked * (claim.HourlyRate * 1.5M), // Use the hourly rate from the claim
				TotalPay = (claim.HoursWorked + claim.OvertimeHoursWorked) * claim.HourlyRate, // Use the hourly rate from the claim

				// Lecturer Notes (added here)
				LecturerNotes = claim.LecturerNotes,

				// Approval details
				ManagerId = claim.Manager?.ManagerID,
				ManagerFullName = $"{claim.Manager?.User.FirstName} {claim.Manager?.User.LastName}",
				ManagerDepartment = claim.Manager?.Department,
				ManagerCampus = claim.Manager?.Campus,
				ApprovalDate = claim.ApprovalDate,
				IsApproved = claim.IsApproved,
				ApprovalComments = claim.ApprovalComments,

				// Verification details
				CoordinatorId = claim.Coordinator?.CoordinatorID,
				CoordinatorFullName = $"{claim.Coordinator?.User.FirstName} {claim.Coordinator?.User.LastName}",
				CoordinatorDepartment = claim.Coordinator?.Department,
				CoordinatorCampus = claim.Coordinator?.Campus,
				VerificationDate = claim.VerificationDate,
				IsVerified = claim.IsVerified,
				VerificationComments = claim.VerificationComments
			};

			return Json(claimDetails);
		}

		[HttpPost]
		public async Task<IActionResult> UploadFilesToClaim(int claimId, IList<IFormFile> documents)
		{
			var claim = await _context.Claims.FirstOrDefaultAsync(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return Json(new
				{
					success = false,
					message = "Claim not found."
				});
			}

			if (documents == null || documents.Count == 0)
			{
				throw new ArgumentException("No files were uploaded.");
			}

			try
			{
				foreach (var document in documents)
				{
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "claims", document.FileName);

					var directoryPath = Path.GetDirectoryName(filePath);
					if (!Directory.Exists(directoryPath))
					{
						Directory.CreateDirectory(directoryPath);
					}

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await document.CopyToAsync(stream);  // Save file to disk
					}

					// Read the file's binary data into a byte array
					byte[] fileData;
					using (var memoryStream = new MemoryStream())
					{
						await document.CopyToAsync(memoryStream);
						fileData = memoryStream.ToArray();  // Load file data into byte array
					}

					// Save file metadata and binary data in the database
					var claimFile = new ClaimFile
					{
						ClaimId = claimId,
						FilePath = filePath,
						FileName = document.FileName,
						FileType = document.ContentType,
						UploadDate = DateTime.UtcNow,
					};

					_context.ClaimFiles.Add(claimFile);
				}

				await _context.SaveChangesAsync();  // Ensure changes are persisted to the database
			}
			catch (Exception ex)
			{
				return Json(new
				{
					success = false,
					message = ex.Message
				});
			}

			return Json(new
			{
				success = true,
				message = "Files uploaded successfully."
			});
		}

		private async Task<byte[]> GetFileBytes(string filePath)
		{
			return await System.IO.File.ReadAllBytesAsync(filePath);
		}

		[HttpGet]
		public IActionResult GetFilesForClaim(int claimId)
		{
			var files = _context.ClaimFiles
				.Where(f => f.ClaimId == claimId)
				.Select(f => new
				{
					f.ClaimFileId,
					f.FileName,
					f.FileType,
					f.UploadDate
				})
				.ToList();

			if (!files.Any())
			{
				return NotFound("No files found for this claim.");
			}

			return Json(files);
		}

		[HttpGet]
		public async Task<IActionResult> DownloadFile(int fileId)
		{
			var file = await _context.ClaimFiles.FindAsync(fileId);

			if (file == null)
			{
				return NotFound("File not found.");
			}

			var filePath = file.FilePath;  // FilePath points to the actual file location on disk

			if (!System.IO.File.Exists(filePath))
			{
				return NotFound("File does not exist on server.");
			}

			var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath); // Read file content

			return File(fileBytes, file.FileType, file.FileName); // Return file as a download
		}

		[HttpPost]
		public IActionResult GenerateTextReport([FromBody] List<Claim> claims)
		{
			if (claims == null || claims.Count == 0)
			{
				return BadRequest("No claims data provided.");
			}

			// Generate the report content
			var reportContent = new StringBuilder();
			reportContent.AppendLine("Claim Report");
			reportContent.AppendLine("Generated on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			reportContent.AppendLine(new string('-', 70));
			reportContent.AppendLine($"{"Claim ID",-10}{"Date & Time",-25}{"Status",-15}");
			reportContent.AppendLine(new string('-', 70));

			foreach (var claim in claims)
			{
				// Ensure safe access to properties
				string claimId = claim.ClaimId.ToString();
				string submissionDate = claim.SubmissionDate.ToString("yyyy-MM-dd HH:mm:ss");
				string status = claim.Status ?? "Unknown";

				// Append claim data including User ID to the report
				reportContent.AppendLine($"{claimId,-10}{submissionDate,-25}{status,-15}");
			}

			// Prepare the file
			var fileName = $"ClaimReport_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
			var fileBytes = Encoding.UTF8.GetBytes(reportContent.ToString());

			// Return the file with a proper Content-Disposition header
			return File(fileBytes, "text/plain", fileName);
		}

	}
}

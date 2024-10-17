﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using ST10257863_PROG6212_POE.Models.Tables;
using System.Collections.Generic;
using System.Linq;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class VerificationController : Controller
	{
		private readonly AppDbContext _context;

		public VerificationController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Verification()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetAllClaims()
		{
			// Retrieve all claims along with associated lecturer and user information
			var claims = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User) // Include User information related to the Lecturer
				.ToList()
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				});

			return Json(claims);
		}

		[HttpGet]
		[Route("Verification/GetClaimDetails/{claimId}")]
		public IActionResult GetClaimDetails(int claimId)
		{
			// Retrieve the claim details along with associated lecturer and user information
			var claim = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User) // Include User information related to the Lecturer
				.FirstOrDefault(c => c.ClaimId == claimId); // Find claim by ID

			if (claim == null)
			{
				return NotFound(); // Return 404 if the claim does not exist
			}

			// Create an anonymous object with the required details
			var claimDetails = new
			{
				LecturerId = claim.Lecturer.LecturerID,
				UserName = claim.Lecturer.User.UserName,
				FullName = $"{claim.Lecturer.User.FirstName} {claim.Lecturer.User.LastName}",
				HourlyRate = claim.Lecturer.HourlyRate,
				Department = claim.Lecturer.Department,
				Campus = claim.Lecturer.Campus,
				RegularHours = claim.HoursWorked,
				OvertimeHours = claim.OvertimeHoursWorked, // Include overtime hours
				TotalHours = claim.HoursWorked + claim.OvertimeHoursWorked, // Calculate total hours
				RegularPay = claim.HoursWorked * claim.Lecturer.HourlyRate,
				OvertimePay = claim.OvertimeHoursWorked * (claim.Lecturer.HourlyRate * 1.5M), // Example overtime rate calculation
				TotalPay = (claim.HoursWorked * claim.Lecturer.HourlyRate) + (claim.OvertimeHoursWorked * claim.Lecturer.HourlyRate * 1.5M)
			};
			return Json(claimDetails); // Return the claim details as JSON
		}

		[HttpPost]
		public IActionResult AcceptClaim(int claimId)
		{
			var claim = _context.Claims
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return NotFound();
			}

			claim.Status = "Accepted";
			_context.SaveChanges();  // Ensure changes are saved

			ViewData["Success"] = "Claim Accepted Successfully.";
			return RedirectToAction("Verification");
		}

		[HttpPost]
		public IActionResult RejectClaim(int claimId)
		{
			var claim = _context.Claims
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return NotFound();
			}

			claim.Status = "Rejected";  // Update status for rejected claims
			_context.SaveChanges();  // Ensure changes are saved

			ViewData["Success"] = "Claim Rejected Successfully.";
			return RedirectToAction("Verification");
		}

	}
}

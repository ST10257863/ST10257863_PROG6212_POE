using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ClaimFile
{
	[Key]
	public int ClaimFileId
	{
		get; set;
	}
	public int ClaimId
	{
		get; set;
	}
	public string FilePath
	{
		get; set;
	}  // Only store path
	public string FileName
	{
		get; set;
	}
	public string FileType
	{
		get; set;
	}
	public DateTime UploadDate
	{
		get; set;
	}

	// If FileData is not needed, remove this field
	// public byte[] FileData { get; set; }
}
using SampleTaskApp.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserInfo
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required, MaxLength(50)]
    public string UserName { get; set; }
    [Password(MinLength = 8, MaxLength = 16, RequireUppercase = true, RequireLowercase = true, RequireDigit = true, RequireSpecialCharacter = true,
              ErrorMessage = "Password does not meet the required criteria.")]
    [Required, MaxLength(255)]
    public string Password { get; set; }

    [Required, MaxLength(50)]
    public string Role { get; set; }

    // Relationships
    public int? DoctorId { get; set; }
    [ForeignKey("DoctorId")]
    public Doctors Doctor { get; set; }

    public int? PatientId { get; set; }
    [ForeignKey("PatientId")]
    public Patients Patient { get; set; }
}
public class UserPermission
{
    [Key]
    public int PermissionId { get; set; }
    public int PageId { get; set; }
    [Required, MaxLength(50)]
    public string ControllerName { get; set; }
    [Required, MaxLength(10)]
    public string MethodName { get; set; }
    public int? UserId { get; set; }
    // Relationships
    [ForeignKey("UserId")]
    public UserInfo UserInfos { get; set; }
}
public class Doctors
{
    [Key]
    public int DoctorId { get; set; }

    [Required, MaxLength(100)]
    public string DoctorName { get; set; }

    [PhoneNumber(ErrorMessage = "Please enter a valid Bangladeshi phone number.")]
    [Required, MaxLength(15)]
    public string DoctorPhone { get; set; }
    [Email(ErrorMessage = "Please enter a valid email address.")]
    [Required, MaxLength(100)]
    public string DoctorEmail { get; set; }
    [MaxMinAge(100, 18, ErrorMessage = "Age must be between 18 and 100.")]
    [Required, MaxLength(3)]
    public string DoctorAge { get; set; }

    public int HospitalId { get; set; }
    [ForeignKey("HospitalId")]
    public Hospitals Hospital { get; set; }
}

public class Hospitals
{
    [Key]
    public int HospitalId { get; set; }

    [Required, MaxLength(200)]
    public string HospitalName { get; set; }

    [Required, MaxLength(300)]
    public string HospitalLocation { get; set; }

    // Relationships
    public ICollection<Doctors> Doctors { get; set; }
    public ICollection<Beds> Beds { get; set; }
}

public class Beds
{
    [Key]
    public int BedId { get; set; }

    [Required, MaxLength(100)]
    public string BedName { get; set; }

    [Required, MaxLength(300)]
    public string BedLocation { get; set; }

    public int HospitalId { get; set; }
    [ForeignKey("HospitalId")]
    public Hospitals Hospital { get; set; }

    // Relationships
    public ICollection<BedsAlotements> BedAllotments { get; set; }
}

public class Patients
{
    [Key]
    public int PatientId { get; set; }

    [Required, MaxLength(100)]
    public string PatientName { get; set; }
    [PhoneNumber(ErrorMessage = "Please enter a valid Bangladeshi phone number.")]
    [Required, MaxLength(15)]
    public string PatientPhone { get; set; }
    [Email(ErrorMessage = "Please enter a valid email address.")]
    [Required, MaxLength(100)]
    public string PatientEmail { get; set; }

    [Required, MaxLength(3)]
    public string PatientAge { get; set; }

    [Required, MaxLength(300)]
    public string PatientAddress { get; set; }

  
  
}

public class BedsAlotements
{
    [Key]
    public int AlotementId { get; set; }

    public int PatientId { get; set; }
    [ForeignKey("PatientId")]
    public Patients Patient { get; set; }

    public int BedId { get; set; }
    [ForeignKey("BedId")]
    public Beds Bed { get; set; }

    public int DoctorId { get; set; }
    [ForeignKey("DoctorId")]
    public Doctors Doctor { get; set; }
}

public class Notifications
{
    [Key]
    public int NotificationId { get; set; }

    [Required, MaxLength(200)]
    public string NotificationHeader { get; set; }

    [Required, MaxLength(500)]
    public string NotificationBody { get; set; }

    [MaxLength(300)]
    public string ReturnUrl { get; set; }

    public int? DoctorId { get; set; }
    [ForeignKey("DoctorId")]
    public Doctors Doctor { get; set; }

    public int? PatientId { get; set; }
    [ForeignKey("PatientId")]
    public Patients Patient { get; set; }
}

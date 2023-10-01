using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otto.Storage.City.Entity;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    
    [Required]
    public long CountryId { get; set; }
    public Country? Country { get; set; }
}
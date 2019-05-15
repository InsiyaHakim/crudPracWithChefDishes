using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDpractice.Models
{
    public class User
    {
        [Key]
        public int user_id{get;set;}

        [Required(ErrorMessage="First Name is required!")]
        [MinLength(4,ErrorMessage="Length must be more than 4 chaacters")]
        [Display(Name="First Name")]
        public string f_name{get;set;}

        [Required(ErrorMessage="Last Name is required!")]
        [MinLength(4,ErrorMessage="Length must be more than 4 chaacters")]
        [Display(Name="Last Name")]
        public string l_name{get;set;}

        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string email{get;set;}

        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage="Password must be more than 8 characters")]
        public string password{get;set;}

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("password")]
        public string c_password{get;set;}

        public DateTime created_at{get;set;}=DateTime.Now;
        public DateTime updated_at{get;set;}=DateTime.Now;

    }
    public class Login
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string email{get;set;}

        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string password{get;set;}
    }

    public class Chef
    {
        [Key]
        public int chef_id{get;set;}

        [Required]
        [Display(Name="First Name")]
        public string f_name{get;set;}

        [Required]
        [Display(Name="Last Name")]
        public string l_name{get;set;}

        [Required]
        [Display(Name="Age")]
        public string age{get;set;}

        public List<Dishes> Dishes{get;set;}

        public string full_name
        {
            get{return f_name +" "+l_name;}
        }
    }

    public class Dishes
    {
        [Key]
        public int dish_id{get;set;}

        [Required]
        [Display(Name="Name of Dish")]
        public string name{get;set;}

        [Required]
        [Display(Name="# of Calories")]
        public float calories{get;set;}

        [Required]
        [Display(Name="Description")]
        public string description{get;set;}

        public int chef_id{get;set;}

        public Chef Chef {get;set;}
    }
}
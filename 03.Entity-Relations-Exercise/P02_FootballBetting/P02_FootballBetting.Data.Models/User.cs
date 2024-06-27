﻿using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
        }

        // it is good to be GUID
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserUsernameMaxLength)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string Name { get; set; } = null!;


        // password are saved heshed in the DB
        [Required]
        [MaxLength(ValidationConstants.UserPasswordMaxLength)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.UserEmailMaxLength)]
        public string Email { get; set; } = null!;

        public decimal Balance { get; set; }



        // Bet relations
        public virtual ICollection<Bet> Bets { get; set; }

    }
}

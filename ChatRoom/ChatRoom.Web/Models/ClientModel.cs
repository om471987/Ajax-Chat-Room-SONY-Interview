using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace ChatRoom.Web.Models
{
    /// <summary>
    /// stores user information
    /// </summary>
    public class ClientModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9-]*$", ErrorMessage = "Name can only contain a-z, A-Z, 0-9 and -")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Name must be between 6 and 15 characters.")]
        public string Name { get; set; }
    }
}

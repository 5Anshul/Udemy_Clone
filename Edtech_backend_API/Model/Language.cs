using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Model
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public ICollection<Course>Courses { get; set; }
    }
}

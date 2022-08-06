using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TargetAPI.Models
{
   public class HeroViewModel
   {
      public int id_hero { get; set; }
      public string name { get; set; }
      public string realName { get; set; }
      public string universe { get; set; }
   }
}
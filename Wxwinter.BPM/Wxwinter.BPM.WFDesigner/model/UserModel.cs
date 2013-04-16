using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wxwinter.BPM.WFDesigner.model
{
    public class UserModel
    {


        public enum Gender { Male, Female }
        public string ID { get; set; }

   
        public string Name { get; set; }

      
        public string Department { get; set; }

   
        public string Email { get; set; }

     
        public Gender PersonGender { get; set; }

        public String PersonPosition { get; set; }

     
        public String Telephone { get; set; }

    
        public String PersonDuty { get; set; }
    }
}

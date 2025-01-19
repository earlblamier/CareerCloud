/// <summary>
/// Represents the education information of an applicant.
/// </summary>
/// 
/// <author>Earl Lamier</author>
/// <studentaccount>N01710966</studentaccount>
/// <email>earlblamier@gmail.com</email>
/// <created>2024-10-29</created>
/// <course>Full Stack .NET Cloud Developer</course>
/// <school>Humber Polytechnic</school>
/// <instructor>Amandeep Patti</instructor>
/// <assignment>Assignment 1</assignment>
/// <dueDate>2024-10-31</dueDate>

using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    public interface IPoco
    {
        Guid Id { get; set; }
    }
}

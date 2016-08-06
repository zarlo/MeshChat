using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Common
{
    public class Login
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public class UserData
    {
        public string sessid { get; set; }
        public string session_name { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }
    public class User
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string theme { get; set; }
        public string signature { get; set; }
        public string signature_format { get; set; }
        public string created { get; set; }
        public string access { get; set; }
        public int login { get; set; }
        public string status { get; set; }
        public string timezone { get; set; }
        public string language { get; set; }
        public object picture { get; set; }
        public string init { get; set; }
        public Data data { get; set; }

        public RdfMapping rdf_mapping { get; set; }
    }
    public class RdfMapping
    {
        public IList<string> rdftype { get; set; }
        public Name name { get; set; }
        public Homepage homepage { get; set; }
    }
    public class Homepage
    {
        public IList<string> predicates { get; set; }
        public string type { get; set; }
    }
    public class Name
    {
        public IList<string> predicates { get; set; }
    }

    public class Data
{
    public string ckeditor_default { get; set; }
    public string ckeditor_show_toggle { get; set; }
    public string ckeditor_width { get; set; }
    public string ckeditor_lang { get; set; }
    public string ckeditor_auto_lang { get; set; }
    public int contact { get; set; }
    public int overlay { get; set; }
}

}

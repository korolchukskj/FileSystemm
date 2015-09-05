using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication23.Models;
using System.IO;

namespace WebApplication23.Controllers
{

    public class FilesController : ApiController
    {
        static string p;
        /// <summary>
        ///  Struct
        /// for all info for simple page
        /// </summary>
        public struct Inform
        {
            public List<Info> inf;//Name and Size
            public string S; //Current path or messege about error 
            public bool show; // Set what element show 
            public int small, normal, big; // Counts for size of file
        }
        public Inform data=new Inform();
        List<Info> inf = new List<Info>();
        // GET api/values

        /// <summary>
        ///  Method
        /// for get data without special value
        /// </summary>
        public Inform Get()
        { 
            data.S = "Початок роботи";
            data.show = false;
            data.inf = DeviceAbout();
            p = null;
            return data;
        }

        /// <summary>
        ///  Method
        /// show all memory device on PC
        /// </summary>
        public List<Info> DeviceAbout()
        {
            string[] drives = Directory.GetLogicalDrives();  
            foreach (string str in drives)
            {


              inf.Add(new Info { Name = str, Size = "p" });//p in Size use for show that it is not a file
            }
            return inf;
        }

        /// <summary>
        ///  Method
        /// show all folder and file  on take folder
        /// </summary>
        public void FileAbout(string path)
        {

            
         try{
                
                DirectoryInfo dir = new DirectoryInfo(p);
                FileInfo[] imageFiles = dir.GetFiles();
                DirectoryInfo[] image = dir.GetDirectories();
                foreach (DirectoryInfo f in image)
                {
                    inf.Add(new Info { Name = f.Name, Size = "p" });//p in Size use for show that it is not a file

                }

                foreach (FileInfo f in imageFiles)
                {
                    double realsize = (double)f.Length / 1024 / 1024; //for convert size from bite to mb
                    inf.Add(new Info { Name = f.Name, Size = realsize.ToString() });
                    if ((realsize) <= 10)
                    {
                        data.small++;
                    }
                    if ((realsize) > 10 && (realsize) <= 50)
                    {
                        data.normal++;
                            
                    }
                    if ((realsize) >= 100)
                    {
                        data.big++;
                    }

                }
                data.inf = inf;
                data.S = p;
                data.show = true;
       }
             //for catch error if device not ready
             catch (DirectoryNotFoundException e)
             {
                  Get();
             }
             catch(NotSupportedException e)
         {
            
             Get();
             data.S = "Пристрій не готовий";

             }
         catch (IOException e)
         {
            
             Get();
             data.S = "Пристрій не готовий";

             }
            
        }
        
     
        // GET api/values/5

        /// <summary>
        ///  Method
        /// for get data with special value
        /// </summary>
        public Inform Get(string p1,string p2)
        {
            if (p2 == "0")//for data in folder
            {
                p = p + p1 + @"\";
                FileAbout(p); 
            }
            else//for data when we go out from folder
            {
                if (p.Length > 4)
                {
                    p = p.Remove(p.Length - 1);
                    char last = p[p.Length - 1];
                    while (last != '\\' && p.Length > 4)
                    {

                        last = p[p.Length - 1];
                        p = p.Remove(p.Length - 1);

                    }
                    FileAbout(p);
                }
                else
                {
                    Get();
                }
                }

            return data;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

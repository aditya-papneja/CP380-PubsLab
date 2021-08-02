using System;
using System.Collections.Generic;
using System.Linq;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connected");
                }

                // 1:Many practice
                //
                // TODO: - Loop through each employee
                //       - For each employee, list their job description (job_desc, in the jobs table)
                List<string> Jd = new List<string>();
                var employees = dbcontext.Employees;
                foreach(var emp in employees)
                {
                    Jd.Add(dbcontext.JobsList.Find(emp.emp_id).job_desc);
                }

                // TODO: - Loop through all of the jobs
                //       - For each job, list the employees (first name, last name) that have that job

                List<Tuple<string, string>> empDetails = new List<Tuple<string, string>>();
                var jobs = dbcontext.JobsList;
                foreach (var job in jobs)
                {
                    var temp = dbcontext.Employees.Find(job.job_id);
                    empDetails.Add(new Tuple<string, string>(temp.fname, temp.lname));
                }

                // Many:many practice
                //
                // TODO: - Loop through each Store
                //       - For each store, list all the titles sold at that store
                //
                // e.g.
                //  Bookbeat -> The Gourmet Microwave, The Busy Executive's Database Guide, Cooking with Computers: Surreptitious Balance Sheets, But Is It User Friendly?

                Dictionary<int, List<string>> titles = new Dictionary<int, List<string>>();
                var stores = dbcontext.StoresList;
                foreach(var st in stores)
                {
                    if(titles.ContainsKey(st.stor_id))
                    {
                        var temp = titles[st.stor_id];
                        temp.AddRange(st.Titles.Select(x=> x.title).ToArray());
                        titles[st.stor_id] = temp;
                    }
                    else
                    {
                        titles.Add(st.stor_id,new List<string>(st.Titles.Select(x=> x.title).ToArray()));
                    }
                }


                // TODO: - Loop through each Title
                //       - For each title, list all the stores it was sold at
                //
                // e.g.
                //  The Gourmet Microwave -> Doc-U-Mat: Quality Laundry and Books, Bookbeat

                Dictionary<string, List<string>> store = new Dictionary<string, List<string>>();
                var titleList = dbcontext.TitlesList;
                foreach (var title in titleList)
                {
                    if (store.ContainsKey(title.title_id))
                    {
                        var temp = store[title.title_id];
                        temp.AddRange(titleList.Where(x => x.title_id == title.title_id).Select(x => x.store.stor_name).ToArray());
                        store[title.title_id] = temp;
                    }
                    else
                    {
                        store.Add(title.title_id, new List<string>(titleList.Where(x => x.title_id == title.title_id).Select(x => x.store.stor_name).ToArray()));
                    }
                }

                Console.ReadLine();
            }
        }
    }
}

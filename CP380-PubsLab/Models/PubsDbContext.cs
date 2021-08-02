using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CP380_PubsLab.Models
{
    public class PubsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\pubs.mdf"));
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Integrated Security=true;AttachDbFilename={dbpath}");
        }

        // TODO: Add DbSets
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Jobs> JobsList { get; set; }
        public virtual DbSet<Titles> TitlesList { get; set; }
        public virtual DbSet<Stores> StoresList { get; set; }
        public virtual DbSet<Sales> SalesList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO
            modelBuilder.Entity<Employee>(x =>
            {
                x.HasIndex(e => e.emp_id);
            });

            modelBuilder.Entity<Jobs>(e =>
            {
                e.HasIndex(x => x.job_id);
            });

            modelBuilder.Entity<Sales>(e =>
            {
                e.HasIndex(x => x.ord_num);
            });

            modelBuilder.Entity<Titles>(e =>
            {
                e.HasIndex(x => x.pub_id);
            });

            modelBuilder.Entity<Stores>(e =>
            {
                e.HasIndex(x => x.stor_id);
            });
        }
    }


    public class Titles
    {
        // TODO
        [Key]
        public string title_id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public int pub_id { get; set; }
        public double price { get; set; }
        public double advance { get; set; }
        public int royalty { get; set; }
        public int ytd_sales { get; set; }
        public string notes { get; set; }
        public DateTime pubdate { get; set; }
        public Stores store { get; set; }
    }


    public class Stores
    {
        // TODO
        [Key]
        public int stor_id { get; set; }    
        public string stor_name { get; set; }  
        public string stor_address { get; set; }
        public string city { get; set; }       
        public string state { get; set; }      
        public int zip { get; set; }
        public List<Titles> Titles { get; set; }
    }


    public class Sales
    {
        // TODO
        [Key]
        public int stor_id { get; set; }
        public string ord_num { get; set; }
        public DateTime ord_date { get; set; }
        public int qty { get; set; }
        public string payterms { get; set; }
        public string title_id { get; set; }
    }

    public class Employee
    {
        // TODO
        [Key]
        [Required]
        public string emp_id { get; set; }
        public string fname { get; set; }
        public char minit { get; set; }
        public string lname { get; set; }
        public int job_id { get; set; }
        public int job_lvl { get; set; }
        public int pub_id { get; set; }
        public DateTime hire_date { get; set; }
        public Jobs currentJob { get; set; }
    }

    public class Jobs
    {
        // TODO
        [Key]
        [Required]
        public int job_id { get; set; } 
        public string job_desc { get; set; }
        public int min_lvl { get; set; }
        public int max_lvl { get; set; }
        public ICollection<Employee> employees { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RepairShopAPI.Models;

public partial class RepairShopContext : DbContext
{
    public RepairShopContext()
    {
    }

    public RepairShopContext(DbContextOptions<RepairShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customerprofile> Customerprofiles { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderevent> Orderevents { get; set; }

    public virtual DbSet<Orderpart> Orderparts { get; set; }

    public virtual DbSet<Orderservice> Orderservices { get; set; }

    public virtual DbSet<Orderstatus> Orderstatuses { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Technicianprofile> Technicianprofiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customerprofile>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("customerprofile_pkey");

            entity.ToTable("customerprofile");

            entity.Property(e => e.Userid)
                .ValueGeneratedNever()
                .HasColumnName("userid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Preferredcontactmethod).HasColumnName("preferredcontactmethod");

            entity.HasOne(d => d.User).WithOne(p => p.Customerprofile)
                .HasForeignKey<Customerprofile>(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customerprofile_userid_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.HasIndex(e => e.Orderid, "invoice_orderid_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Dueat).HasColumnName("dueat");
            entity.Property(e => e.Ispaid)
                .HasDefaultValue(false)
                .HasColumnName("ispaid");
            entity.Property(e => e.Issuedat)
                .HasDefaultValueSql("now()")
                .HasColumnName("issuedat");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Paidamount)
                .HasPrecision(14, 2)
                .HasColumnName("paidamount");
            entity.Property(e => e.Totalamount)
                .HasPrecision(14, 2)
                .HasColumnName("totalamount");

            entity.HasOne(d => d.Order).WithOne(p => p.Invoice)
                .HasForeignKey<Invoice>(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_orderid_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Completedat).HasColumnName("completedat");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Externalreference).HasColumnName("externalreference");
            entity.Property(e => e.Masterid).HasColumnName("masterid");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Scheduledat).HasColumnName("scheduledat");
            entity.Property(e => e.Startedat).HasColumnName("startedat");
            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Totalprice)
                .HasPrecision(14, 2)
                .HasColumnName("totalprice");

            entity.HasOne(d => d.Client).WithMany(p => p.OrderClients)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_clientid_fkey");

            entity.HasOne(d => d.Master).WithMany(p => p.OrderMasters)
                .HasForeignKey(d => d.Masterid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_masterid_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Statusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_statusid_fkey");
        });

        modelBuilder.Entity<Orderevent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderevent_pkey");

            entity.ToTable("orderevent");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Actoruserid).HasColumnName("actoruserid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Eventdata)
                .HasColumnType("jsonb")
                .HasColumnName("eventdata");
            entity.Property(e => e.Eventtype).HasColumnName("eventtype");
            entity.Property(e => e.Orderid).HasColumnName("orderid");

            entity.HasOne(d => d.Actoruser).WithMany(p => p.Orderevents)
                .HasForeignKey(d => d.Actoruserid)
                .HasConstraintName("orderevent_actoruserid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderevents)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderevent_orderid_fkey");
        });

        modelBuilder.Entity<Orderpart>(entity =>
        {
            entity.HasKey(e => new { e.Orderid, e.Partid }).HasName("orderpart_pkey");

            entity.ToTable("orderpart");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Partid).HasColumnName("partid");
            entity.Property(e => e.Priceattime)
                .HasPrecision(12, 2)
                .HasColumnName("priceattime");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderparts)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderpart_orderid_fkey");

            entity.HasOne(d => d.Part).WithMany(p => p.Orderparts)
                .HasForeignKey(d => d.Partid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderpart_partid_fkey");
        });

        modelBuilder.Entity<Orderservice>(entity =>
        {
            entity.HasKey(e => new { e.Orderid, e.Serviceid }).HasName("orderservice_pkey");

            entity.ToTable("orderservice");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Serviceid).HasColumnName("serviceid");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Priceattime)
                .HasPrecision(12, 2)
                .HasColumnName("priceattime");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderservice_orderid_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Serviceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderservice_serviceid_fkey");
        });

        modelBuilder.Entity<Orderstatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderstatus_pkey");

            entity.ToTable("orderstatus");

            entity.HasIndex(e => e.Code, "orderstatus_code_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("part_pkey");

            entity.ToTable("part");

            entity.HasIndex(e => e.Sku, "part_sku_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Sku).HasColumnName("sku");
            entity.Property(e => e.Stockquantity)
                .HasDefaultValue(0)
                .HasColumnName("stockquantity");
            entity.Property(e => e.Unitprice)
                .HasPrecision(12, 2)
                .HasColumnName("unitprice");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.Paidat)
                .HasDefaultValueSql("now()")
                .HasColumnName("paidat");
            entity.Property(e => e.Reference).HasColumnName("reference");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Invoiceid)
                .HasConstraintName("payment_invoiceid_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "Role_name_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("service");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Durationminutes).HasColumnName("durationminutes");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Technicianprofile>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("technicianprofile_pkey");

            entity.ToTable("technicianprofile");

            entity.HasIndex(e => e.Employeenumber, "technicianprofile_employeenumber_key").IsUnique();

            entity.Property(e => e.Userid)
                .ValueGeneratedNever()
                .HasColumnName("userid");
            entity.Property(e => e.Certifications).HasColumnName("certifications");
            entity.Property(e => e.Employeenumber).HasColumnName("employeenumber");
            entity.Property(e => e.Hiredate).HasColumnName("hiredate");
            entity.Property(e => e.Hourlyrate)
                .HasPrecision(12, 2)
                .HasColumnName("hourlyrate");
            entity.Property(e => e.Isavailable)
                .HasDefaultValue(true)
                .HasColumnName("isavailable");

            entity.HasOne(d => d.User).WithOne(p => p.Technicianprofile)
                .HasForeignKey<Technicianprofile>(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("technicianprofile_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Roleid).HasColumnName("roleid");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_roleid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<Devicebrand> Devicebrands { get; set; }

    public virtual DbSet<Devicemodel> Devicemodels { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Modelvariant> Modelvariants { get; set; }

    public virtual DbSet<Orderassignment> Orderassignments { get; set; }

    public virtual DbSet<Orderpart> Orderparts { get; set; }

    public virtual DbSet<Orderservice> Orderservices { get; set; }

    public virtual DbSet<Orderstatus> Orderstatuses { get; set; }

    public virtual DbSet<Ordertotal> Ordertotals { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Processor> Processors { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Repairorder> Repairorders { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.HasIndex(e => e.Phone, "idx_clients_phone");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("devices_pkey");

            entity.ToTable("devices");

            entity.HasIndex(e => e.Clientid, "idx_devices_clientid");

            entity.HasIndex(e => e.Modelid, "idx_devices_modelid");

            entity.HasIndex(e => e.Variantid, "idx_devices_variantid");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Imeiorserial).HasColumnName("imeiorserial");
            entity.Property(e => e.Modelid).HasColumnName("modelid");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Purchasedate).HasColumnName("purchasedate");
            entity.Property(e => e.Variantid).HasColumnName("variantid");

            entity.HasOne(d => d.Client).WithMany(p => p.Devices)
                .HasForeignKey(d => d.Clientid)
                .HasConstraintName("devices_clientid_fkey");

            entity.HasOne(d => d.Model).WithMany(p => p.Devices)
                .HasForeignKey(d => d.Modelid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("devices_modelid_fkey");

            entity.HasOne(d => d.Variant).WithMany(p => p.Devices)
                .HasForeignKey(d => d.Variantid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("devices_variantid_fkey");
        });

        modelBuilder.Entity<Devicebrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("devicebrands_pkey");

            entity.ToTable("devicebrands");

            entity.HasIndex(e => e.Brandname, "devicebrands_brandname_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Brandname).HasColumnName("brandname");
        });

        modelBuilder.Entity<Devicemodel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("devicemodels_pkey");

            entity.ToTable("devicemodels");

            entity.HasIndex(e => new { e.Brandid, e.Modelname }, "devicemodels_brandid_modelname_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Brandid).HasColumnName("brandid");
            entity.Property(e => e.Modelname).HasColumnName("modelname");

            entity.HasOne(d => d.Brand).WithMany(p => p.Devicemodels)
                .HasForeignKey(d => d.Brandid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("devicemodels_brandid_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Hiredat).HasColumnName("hiredat");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        modelBuilder.Entity<Modelvariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("modelvariants_pkey");

            entity.ToTable("modelvariants");

            entity.HasIndex(e => new { e.Modelid, e.Variantname }, "modelvariants_modelid_variantname_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Modelid).HasColumnName("modelid");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Processorid).HasColumnName("processorid");
            entity.Property(e => e.Ramgb).HasColumnName("ramgb");
            entity.Property(e => e.Regioncode).HasColumnName("regioncode");
            entity.Property(e => e.Storagegb).HasColumnName("storagegb");
            entity.Property(e => e.Variantname).HasColumnName("variantname");

            entity.HasOne(d => d.Model).WithMany(p => p.Modelvariants)
                .HasForeignKey(d => d.Modelid)
                .HasConstraintName("modelvariants_modelid_fkey");

            entity.HasOne(d => d.Processor).WithMany(p => p.Modelvariants)
                .HasForeignKey(d => d.Processorid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("modelvariants_processorid_fkey");

            entity.HasOne(d => d.RegioncodeNavigation).WithMany(p => p.Modelvariants)
                .HasForeignKey(d => d.Regioncode)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("modelvariants_regioncode_fkey");
        });

        modelBuilder.Entity<Orderassignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderassignments_pkey");

            entity.ToTable("orderassignments");

            entity.HasIndex(e => e.Employeeid, "idx_orderassignments_employeeid");

            entity.HasIndex(e => new { e.Orderid, e.Employeeid }, "orderassignments_orderid_employeeid_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Assignedat)
                .HasDefaultValueSql("now()")
                .HasColumnName("assignedat");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orderassignments)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("orderassignments_employeeid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderassignments)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderassignments_orderid_fkey");
        });

        modelBuilder.Entity<Orderpart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderparts_pkey");

            entity.ToTable("orderparts");

            entity.HasIndex(e => e.Orderid, "idx_orderparts_orderid");

            entity.HasIndex(e => e.Partid, "idx_orderparts_partid");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Partid).HasColumnName("partid");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.Unitprice)
                .HasPrecision(10, 2)
                .HasColumnName("unitprice");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderparts)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderparts_orderid_fkey");

            entity.HasOne(d => d.Part).WithMany(p => p.Orderparts)
                .HasForeignKey(d => d.Partid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("orderparts_partid_fkey");
        });

        modelBuilder.Entity<Orderservice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderservices_pkey");

            entity.ToTable("orderservices");

            entity.HasIndex(e => e.Orderid, "idx_orderservices_orderid");

            entity.HasIndex(e => e.Serviceid, "idx_orderservices_serviceid");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.Serviceid).HasColumnName("serviceid");
            entity.Property(e => e.Unitprice)
                .HasPrecision(10, 2)
                .HasColumnName("unitprice");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderservices_orderid_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Serviceid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("orderservices_serviceid_fkey");
        });

        modelBuilder.Entity<Orderstatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderstatuses_pkey");

            entity.ToTable("orderstatuses");

            entity.HasIndex(e => e.Statuscode, "orderstatuses_statuscode_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Displayname).HasColumnName("displayname");
            entity.Property(e => e.Statuscode).HasColumnName("statuscode");
        });

        modelBuilder.Entity<Ordertotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ordertotals");

            entity.Property(e => e.Createdat).HasColumnName("createdat");
            entity.Property(e => e.Grandtotal).HasColumnName("grandtotal");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Partstotal).HasColumnName("partstotal");
            entity.Property(e => e.Servicestotal).HasColumnName("servicestotal");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parts_pkey");

            entity.ToTable("parts");

            entity.HasIndex(e => e.Partsku, "parts_partsku_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Compatiblemodelid).HasColumnName("compatiblemodelid");
            entity.Property(e => e.Partname).HasColumnName("partname");
            entity.Property(e => e.Partsku).HasColumnName("partsku");
            entity.Property(e => e.Stockqty)
                .HasDefaultValue(0)
                .HasColumnName("stockqty");
            entity.Property(e => e.Unitcost)
                .HasPrecision(10, 2)
                .HasColumnName("unitcost");

            entity.HasOne(d => d.Compatiblemodel).WithMany(p => p.Parts)
                .HasForeignKey(d => d.Compatiblemodelid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("parts_compatiblemodelid_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.HasIndex(e => e.Orderid, "idx_payments_orderid");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Paidat)
                .HasDefaultValueSql("now()")
                .HasColumnName("paidat");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("payments_orderid_fkey");
        });

        modelBuilder.Entity<Processor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("processors_pkey");

            entity.ToTable("processors");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Baseclockghz)
                .HasPrecision(4, 2)
                .HasColumnName("baseclockghz");
            entity.Property(e => e.Cores).HasColumnName("cores");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("regions_pkey");

            entity.ToTable("regions");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Repairorder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("repairorders_pkey");

            entity.ToTable("repairorders");

            entity.HasIndex(e => e.Createdat, "idx_repairorders_createdat");

            entity.HasIndex(e => e.Deviceid, "idx_repairorders_deviceid");

            entity.HasIndex(e => e.Statusid, "idx_repairorders_statusid");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Closedat).HasColumnName("closedat");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerdescription).HasColumnName("customerdescription");
            entity.Property(e => e.Deviceid).HasColumnName("deviceid");
            entity.Property(e => e.Diagnosticnotes).HasColumnName("diagnosticnotes");
            entity.Property(e => e.Expectedreadyat).HasColumnName("expectedreadyat");
            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Totalcost)
                .HasPrecision(12, 2)
                .HasColumnName("totalcost");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("now()")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Device).WithMany(p => p.Repairorders)
                .HasForeignKey(d => d.Deviceid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("repairorders_deviceid_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Repairorders)
                .HasForeignKey(d => d.Statusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("repairorders_statusid_fkey");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("services_pkey");

            entity.ToTable("services");

            entity.HasIndex(e => e.Servicecode, "services_servicecode_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Defaultprice)
                .HasPrecision(10, 2)
                .HasColumnName("defaultprice");
            entity.Property(e => e.Laborminutes).HasColumnName("laborminutes");
            entity.Property(e => e.Servicecode).HasColumnName("servicecode");
            entity.Property(e => e.Servicename).HasColumnName("servicename");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

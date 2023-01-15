using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RPPP21APP.Models;


namespace RPPP21APP.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<ActionM> Actions { get; set; }

    public virtual DbSet<ActionOnGroup> ActionOnGroups { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Contractor> Contractors { get; set; }

    public virtual DbSet<CostIncurred> CostIncurreds { get; set; }

    public virtual DbSet<CostPerGroup> CostPerGroups { get; set; }

    public virtual DbSet<CostType> CostTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<DeliveryType> DeliveryTypes { get; set; }

    public virtual DbSet<Grant> Grants { get; set; }

    public virtual DbSet<GroupOfPlant> GroupOfPlants { get; set; }

    public virtual DbSet<GroupOfPlantsReservation> GroupOfPlantsReservations { get; set; }

    public virtual DbSet<GroupsOnPlot> GroupsOnPlots { get; set; }

    public virtual DbSet<HistoricalInfrastructure> HistoricalInfrastructures { get; set; }

    public virtual DbSet<Infrastructure> Infrastructures { get; set; }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<LeaseType> LeaseTypes { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialUse> MaterialUses { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<PlantType> PlantTypes { get; set; }

    public virtual DbSet<Plot> Plots { get; set; }

    public virtual DbSet<PlotDelivery> PlotDeliveries { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    public virtual DbSet<WeatherCondition> WeatherConditions { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActionM>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("Action_pk");

            entity.ToTable("Action", "RPPP21");

            entity.HasIndex(e => e.ActionId, "Action_ActionID_uindex").IsUnique();

            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.Description).HasColumnType("text");
        });

        modelBuilder.Entity<ActionOnGroup>(entity =>
        {
            entity.HasKey(e => e.ActionOnGroupId).HasName("ActionOnGroup_pk");

            entity.ToTable("ActionOnGroup", "RPPP21");

            entity.HasIndex(e => e.ActionOnGroupId, "ActionOnGroup_ActionOnGroupID_uindex").IsUnique();

            entity.Property(e => e.ActionOnGroupId).HasColumnName("ActionOnGroupID");
            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.GroupOfPlantsId).HasColumnName("GroupOfPlantsID");
            entity.Property(e => e.MaterialUseId).HasColumnName("MaterialUseID");
            entity.Property(e => e.StorageId).HasColumnName("StorageID");
            entity.Property(e => e.Time).HasColumnType("date");
            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

            entity.HasOne(d => d.Action).WithMany(p => p.ActionOnGroups)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActionOnGroup_Action_ActionID_fk");

            entity.HasOne(d => d.GroupOfPlants).WithMany(p => p.ActionOnGroups)
                .HasForeignKey(d => d.GroupOfPlantsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActionOnGroup_GroupOfPlants_GroupOfPlantsID_fk");

            entity.HasOne(d => d.MaterialUse).WithMany(p => p.ActionOnGroups)
                .HasForeignKey(d => d.MaterialUseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActionOnGroup_MaterialUse_MaterialUseID_fk");

            entity.HasOne(d => d.Storage).WithMany(p => p.ActionOnGroups)
                .HasForeignKey(d => d.StorageId)
                .HasConstraintName("ActionOnGroup_Storage_StorageID_fk");

            entity.HasOne(d => d.Worker).WithMany(p => p.ActionOnGroups)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActionOnGroup_Worker_WorkerID_fk");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("Contract_pk");

            entity.ToTable("Contract", "RPPP21");

            entity.HasIndex(e => e.ContractId, "Contract_ContractID_uindex").IsUnique();

            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.ContractorId).HasColumnName("ContractorID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");

            entity.HasOne(d => d.Contractor).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ContractorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contract_Contractor_ContractorID_fk");
        });

        modelBuilder.Entity<Contractor>(entity =>
        {
            entity.HasKey(e => e.ContractorId).HasName("Contractor_pk");

            entity.ToTable("Contractor", "RPPP21");

            entity.HasIndex(e => e.ContractorId, "Contractor_ContractorID_uindex").IsUnique();

            entity.Property(e => e.ContractorId).HasColumnName("ContractorID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.Email).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.PhoneNumber).HasColumnType("text");
            entity.Property(e => e.Surname).HasColumnType("text");
        });

        modelBuilder.Entity<CostIncurred>(entity =>
        {
            entity.HasKey(e => e.CostIncurredId).HasName("CostIncurred_pk");

            entity.ToTable("CostIncurred", "RPPP21");

            entity.HasIndex(e => e.CostIncurredId, "CostIncurred_CostIncurredID_uindex").IsUnique();

            entity.Property(e => e.CostIncurredId).HasColumnName("CostIncurredID");
            entity.Property(e => e.CostPerGroupId).HasColumnName("CostPerGroupID");
            entity.Property(e => e.CostTypesId).HasColumnName("CostTypesID");
            entity.Property(e => e.Description).HasColumnType("text");

            entity.HasOne(d => d.CostPerGroup).WithMany(p => p.CostIncurreds)
                .HasForeignKey(d => d.CostPerGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CostIncurred_CostPerGroup_CostPerGroupID_fk");

            entity.HasOne(d => d.CostTypes).WithMany(p => p.CostIncurreds)
                .HasForeignKey(d => d.CostTypesId)
                .HasConstraintName("CostIncurred_CostTypes_CostTypesID_fk");
        });

        modelBuilder.Entity<CostPerGroup>(entity =>
        {
            entity.HasKey(e => e.CostPerGroupId).HasName("CostPerGroup_pk");

            entity.ToTable("CostPerGroup", "RPPP21");

            entity.HasIndex(e => e.CostPerGroupId, "CostPerGroup_CostPerGroupID_uindex").IsUnique();

            entity.Property(e => e.CostPerGroupId).HasColumnName("CostPerGroupID");
            entity.Property(e => e.GroupOfPlantsId).HasColumnName("GroupOfPlantsID");

            entity.HasOne(d => d.GroupOfPlants).WithMany(p => p.CostPerGroups)
                .HasForeignKey(d => d.GroupOfPlantsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CostPerGroup_GroupOfPlants_GroupOfPlantsID_fk");
        });

        modelBuilder.Entity<CostType>(entity =>
        {
            entity.HasKey(e => e.CostTypesId).HasName("CostTypes_pk");

            entity.ToTable("CostTypes", "RPPP21");

            entity.HasIndex(e => e.CostTypesId, "CostTypes_CostTypesID_uindex").IsUnique();

            entity.Property(e => e.CostTypesId).HasColumnName("CostTypesID");
            entity.Property(e => e.Name).HasColumnType("text");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("Customer_pk");

            entity.ToTable("Customer", "RPPP21");

            entity.HasIndex(e => e.CustomerId, "Customer_CustomerID_uindex").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Email).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.PhoneNumber).HasColumnType("text");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("Delivery_pk");

            entity.ToTable("Delivery", "RPPP21");

            entity.HasIndex(e => e.DeliveryId, "Delivery_DeliveryID_uindex").IsUnique();

            entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.DeliveryTypesId).HasColumnName("DeliveryTypesID");

            entity.HasOne(d => d.DeliveryTypes).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.DeliveryTypesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Delivery_DeliveryTypes_DeliveryTypesID_fk");
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.DeliveryTypesId).HasName("DeliveryTypes_pk");

            entity.ToTable("DeliveryTypes", "RPPP21");

            entity.HasIndex(e => e.DeliveryTypesId, "DeliveryTypes_DeliveryTypesID_uindex").IsUnique();

            entity.Property(e => e.DeliveryTypesId).HasColumnName("DeliveryTypesID");
            entity.Property(e => e.Type).HasColumnType("text");
        });

        modelBuilder.Entity<Grant>(entity =>
        {
            entity.HasKey(e => e.GrantId).HasName("Grant_pk");

            entity.ToTable("Grant", "RPPP21");

            entity.HasIndex(e => e.GrantId, "Grant_GrantID_uindex").IsUnique();

            entity.Property(e => e.GrantId).HasColumnName("GrantID");
            entity.Property(e => e.GroupOfPlantsId).HasColumnName("GroupOfPlantsID");
            entity.Property(e => e.Name).HasColumnType("text");

            entity.HasOne(d => d.GroupOfPlants).WithMany(p => p.Grants)
                .HasForeignKey(d => d.GroupOfPlantsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Grant_GroupOfPlants_GroupOfPlantsID_fk");
        });

        modelBuilder.Entity<GroupOfPlant>(entity =>
        {
            entity.HasKey(e => e.GroupOfPlantsId).HasName("GroupOfPlants_pk");

            entity.ToTable("GroupOfPlants", "RPPP21");

            entity.HasIndex(e => e.GroupOfPlantsId, "GroupOfPlants_GroupOFPlantsID_uindex").IsUnique();

            entity.Property(e => e.GroupOfPlantsId).HasColumnName("GroupOfPlantsID");
            entity.Property(e => e.GroupsOnPlotId).HasColumnName("GroupsOnPlotID");
            entity.Property(e => e.PlantTypeId).HasColumnName("PlantTypeID");

            entity.HasOne(d => d.GroupsOnPlot).WithMany(p => p.GroupOfPlants)
                .HasForeignKey(d => d.GroupsOnPlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupOfPlants_GroupsOnPlot_GroupsOnPlotID_fk");

            entity.HasOne(d => d.PlantType).WithMany(p => p.GroupOfPlants)
                .HasForeignKey(d => d.PlantTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupOfPlants_PlantType_PlantTypeID_fk");
        });

        modelBuilder.Entity<GroupOfPlantsReservation>(entity =>
        {
            entity.HasKey(e => e.GroupOfPlantsReservationId).HasName("GroupOfPlants_Reservation_pk");

            entity.ToTable("GroupOfPlants_Reservation", "RPPP21");

            entity.HasIndex(e => e.GroupOfPlantsReservationId, "GroupOfPlants_Reservation_GroupOfPlants_ReservationID_uindex").IsUnique();

            entity.Property(e => e.GroupOfPlantsReservationId).HasColumnName("GroupOfPlants_ReservationID");
            entity.Property(e => e.GroupOfPlantsId).HasColumnName("GroupOfPlantsID");
            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

            entity.HasOne(d => d.GroupOfPlants).WithMany(p => p.GroupOfPlantsReservations)
                .HasForeignKey(d => d.GroupOfPlantsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupOfPlants_Reservation_GroupOfPlants_GroupOfPlantsID_fk");

            entity.HasOne(d => d.Reservation).WithMany(p => p.GroupOfPlantsReservations)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupOfPlants_Reservation_Reservation_ReservationID_fk");
        });

        modelBuilder.Entity<GroupsOnPlot>(entity =>
        {
            entity.HasKey(e => e.GroupsOnPlotId).HasName("GroupsOnPlot_pk");

            entity.ToTable("GroupsOnPlot", "RPPP21");

            entity.HasIndex(e => e.GroupsOnPlotId, "GroupsOnPlot_GroupsOnPlotID_uindex").IsUnique();

            entity.Property(e => e.GroupsOnPlotId).HasColumnName("GroupsOnPlotID");
            entity.Property(e => e.DePlantTime).HasColumnType("date");
            entity.Property(e => e.PlantTime).HasColumnType("date");
            entity.Property(e => e.PlotId).HasColumnName("PlotID");

            entity.HasOne(d => d.Plot).WithMany(p => p.GroupsOnPlots)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupsOnPlot_Plot_PlotID_fk");
        });

        modelBuilder.Entity<HistoricalInfrastructure>(entity =>
        {
            entity.HasKey(e => e.HistoricalInfrastructureId).HasName("HistoricalInfrastructure_pk");

            entity.ToTable("HistoricalInfrastructure", "RPPP21");

            entity.HasIndex(e => e.HistoricalInfrastructureId, "HistoricalInfrastructure_HistoricalInfrastructureID_uindex").IsUnique();

            entity.Property(e => e.HistoricalInfrastructureId).HasColumnName("HistoricalInfrastructureID");
            entity.Property(e => e.DateOfdestrcution)
                .HasColumnType("date")
                .HasColumnName("DateOFDestrcution");
            entity.Property(e => e.InfrastructureId).HasColumnName("InfrastructureID");
            entity.Property(e => e.ReasonOfDestruction).HasColumnType("text");

            entity.HasOne(d => d.Infrastructure).WithMany(p => p.HistoricalInfrastructures)
                .HasForeignKey(d => d.InfrastructureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HistoricalInfrastructure_Infrastructure_InfrastructureID_fk");
        });

        modelBuilder.Entity<Infrastructure>(entity =>
        {
            entity.HasKey(e => e.InfrastructureId).HasName("Infrastructure_pk");

            entity.ToTable("Infrastructure", "RPPP21");

            entity.HasIndex(e => e.InfrastructureId, "Infrastructure_InfrastructureID_uindex").IsUnique();

            entity.Property(e => e.InfrastructureId).HasColumnName("InfrastructureID");
            entity.Property(e => e.BuildDate).HasColumnType("date");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.PlotId).HasColumnName("PlotID");

            entity.HasOne(d => d.Plot).WithMany(p => p.Infrastructures)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Infrastructure_Plot_PlotID_fk");
        });

        modelBuilder.Entity<Lease>(entity =>
        {
            entity.HasKey(e => e.LeaseId).HasName("Lease_pk");

            entity.ToTable("Lease", "RPPP21");

            entity.HasIndex(e => e.LeaseId, "Lease_LeaseID_uindex").IsUnique();

            entity.Property(e => e.LeaseId).HasColumnName("LeaseID");
            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.LeaseTypeId).HasColumnName("LeaseTypeID");
            entity.Property(e => e.PlotId).HasColumnName("PlotID");

            entity.HasOne(d => d.Contract).WithMany(p => p.Leases)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Lease_Contract_ContractID_fk");

            entity.HasOne(d => d.LeaseType).WithMany(p => p.Leases)
                .HasForeignKey(d => d.LeaseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Lease_LeaseTypes_LeaseTypeID_fk");

            entity.HasOne(d => d.Plot).WithMany(p => p.Leases)
                .HasForeignKey(d => d.PlotId)
                .HasConstraintName("Lease_Plot_PlotID_fk");
        });

        modelBuilder.Entity<LeaseType>(entity =>
        {
            entity.HasKey(e => e.LeaseTypeId).HasName("LeaseTypes_pk");

            entity.ToTable("LeaseTypes", "RPPP21");

            entity.HasIndex(e => e.LeaseTypeId, "LeaseTypes_LeaseTypeID_uindex").IsUnique();

            entity.Property(e => e.LeaseTypeId).HasColumnName("LeaseTypeID");
            entity.Property(e => e.Name).HasColumnType("text");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("Material_pk");

            entity.ToTable("Material", "RPPP21");

            entity.HasIndex(e => e.MaterialId, "Material_MaterialID_uindex").IsUnique();

            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.Name).HasColumnType("text");
        });

        modelBuilder.Entity<MaterialUse>(entity =>
        {
            entity.HasKey(e => e.MaterialUseId).HasName("MaterialUse_pk");

            entity.ToTable("MaterialUse", "RPPP21");

            entity.HasIndex(e => e.MaterialUseId, "MaterialUse_MaterialUseID_uindex").IsUnique();

            entity.Property(e => e.MaterialUseId).HasColumnName("MaterialUseID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialUses)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MaterialUse_Material_MaterialID_fk");
        });

        modelBuilder.Entity<Passport>(entity =>
        {
            entity.HasKey(e => e.PassportId).HasName("Passport_pk");

            entity.ToTable("Passport", "RPPP21");

            entity.HasIndex(e => e.PassportId, "Passport_PassportID_uindex").IsUnique();

            entity.Property(e => e.PassportId).HasColumnName("PassportID");
            entity.Property(e => e.LatinName).HasColumnType("text");
            entity.Property(e => e.LinkToFloraCroatia).HasColumnType("text");
            entity.Property(e => e.MotherFarm).HasColumnType("text");
            entity.Property(e => e.Origin).HasColumnType("text");
            entity.Property(e => e.PlantId).HasColumnName("PlantID");

            entity.HasOne(d => d.Plant).WithOne(p => p.Passport)
                .HasForeignKey<Plant>(d => d.PlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Passport_Species_SpeciesID_fk");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("Species_pk");

            entity.ToTable("Plant", "RPPP21");

            entity.HasIndex(e => e.PlantId, "Species_SpeciesID_uindex").IsUnique();

            entity.Property(e => e.PlantId).HasColumnName("PlantID");
            entity.Property(e => e.GroupOfPlantsId).HasColumnName("GroupOfPlantsID");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.HasOne(d => d.GroupOfPlants).WithMany(p => p.Plants)
                .HasForeignKey(d => d.GroupOfPlantsId)
                .HasConstraintName("Species_GroupOfPlants_GroupOFPlantsID_fk");
            entity.HasOne(d => d.Passport).WithOne(p => p.Plant)
                .HasForeignKey<Passport>(d => d.PassportId)
                .HasConstraintName("Passport_fk");
        });

        modelBuilder.Entity<PlantType>(entity =>
        {
            entity.HasKey(e => e.PlantTypeId).HasName("PlantType_pk");

            entity.ToTable("PlantType", "RPPP21");

            entity.HasIndex(e => e.PlantTypeId, "PlantType_PlantTypeID_uindex").IsUnique();

            entity.Property(e => e.PlantTypeId).HasColumnName("PlantTypeID");
            entity.Property(e => e.Type).HasColumnType("text");
            entity.Property(e => e.Vitamins).HasColumnType("text");
        });

        modelBuilder.Entity<Plot>(entity =>
        {
            entity.HasKey(e => e.PlotId).HasName("Plot_pk");

            entity.ToTable("Plot", "RPPP21");

            entity.HasIndex(e => e.PlotId, "Plot_PlotID_uindex").IsUnique();

            entity.Property(e => e.PlotId).HasColumnName("PlotID");
            entity.Property(e => e.Coordinates).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.WeatherConditionsId).HasColumnName("WeatherConditionsID");

            entity.HasOne(d => d.WeatherConditions).WithMany(p => p.Plots)
                .HasForeignKey(d => d.WeatherConditionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plot_WeatherConditions_WeatherConditionsID_fk");
        });

        modelBuilder.Entity<PlotDelivery>(entity =>
        {
            entity.HasKey(e => e.PlotDeliveryId).HasName("Plot_Delivery_pk");

            entity.ToTable("Plot_Delivery", "RPPP21");

            entity.HasIndex(e => e.PlotDeliveryId, "Plot_Delivery_Plot_DeliveryID_uindex").IsUnique();

            entity.Property(e => e.PlotDeliveryId).HasColumnName("Plot_DeliveryID");
            entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");
            entity.Property(e => e.PlotId).HasColumnName("PlotID");

            entity.HasOne(d => d.Delivery).WithMany(p => p.PlotDeliveries)
                .HasForeignKey(d => d.DeliveryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plot_Delivery_Delivery_DeliveryID_fk");

            entity.HasOne(d => d.Plot).WithMany(p => p.PlotDeliveries)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plot_Delivery_Plot_PlotID_fk");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("Reservation_pk");

            entity.ToTable("Reservation", "RPPP21");

            entity.HasIndex(e => e.ReservationId, "Reservation_ReservationID_uindex").IsUnique();

            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reservation_Customer_CustomerID_fk");
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.HasKey(e => e.StorageId).HasName("Storage_pk");

            entity.ToTable("Storage", "RPPP21");

            entity.HasIndex(e => e.StorageId, "Storage_StorageID_uindex").IsUnique();

            entity.Property(e => e.StorageId).HasColumnName("StorageID");
            entity.Property(e => e.Place).HasColumnType("text");
            entity.Property(e => e.PlotId).HasColumnName("PlotID");
            entity.Property(e => e.TimeOfHarvest).HasColumnType("date");

            entity.HasOne(d => d.Plot).WithMany(p => p.Storages)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Storage_Plot_PlotID_fk");

            entity.HasOne(d => d.PlantType).WithMany(p => p.Storages)
                .HasForeignKey(d => d.PlantTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PlantType_fk");

        });

        modelBuilder.Entity<WeatherCondition>(entity =>
        {
            entity.HasKey(e => e.WeatherConditionsId).HasName("WeatherConditions_pk");

            entity.ToTable("WeatherConditions", "RPPP21");

            entity.HasIndex(e => e.WeatherConditionsId, "WeatherConditions_WeatherConditionsID_uindex").IsUnique();

            entity.Property(e => e.WeatherConditionsId).HasColumnName("WeatherConditionsID");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("Worker_pk");

            entity.ToTable("Worker", "RPPP21");

            entity.HasIndex(e => e.WorkerId, "Worker_WorkerID_uindex").IsUnique();

            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");
            entity.Property(e => e.Experience).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.PhoneNumber).HasColumnType("text");
            entity.Property(e => e.Surname).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

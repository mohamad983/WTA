using Domain.Entities.Forms;
using Domain.Entities.Functions;
using Domain.Entities.Metadatas;
using Domain.Entities.Permissions;
using Domain.Entities.RequestLogs;
using Domain.Entities.Requests;
using Domain.Entities.RequestTypes;
using Domain.Entities.RolePermissions;
using Domain.Entities.StepFunctions;
using Domain.Entities.Tickets;
using Domain.Entities.TransitionFunctions;
using Domain.Entities.Users;
using Domain.Entities.WorkFlowActions;
using Domain.Entities.WorkFlowSteps;
using Domain.Entities.WorkFlowStepTransitions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<WorkFlowStepTransition> WorkFlowStepTransition => Set<WorkFlowStepTransition>();
        public DbSet<WorkFlowStep> WorkFlowStep => Set<WorkFlowStep>();
        public DbSet<WorkFlowAction> WorkFlowAction => Set<WorkFlowAction>();
        public DbSet<TransitionFunction> TransitionFunction => Set<TransitionFunction>();
        public DbSet<Ticket> Ticket => Set<Ticket>();
        public DbSet<StepFunction> StepFunction => Set<StepFunction>();

        public DbSet<RolePermission> RolePermission => Set<RolePermission>();

        public DbSet<RequestType> RequestType => Set<RequestType>();

        public DbSet<RequestValue> RequestValue => Set<RequestValue>();

        public DbSet<RequestApproval> RequestApproval => Set<RequestApproval>();

        public DbSet<Request> Request => Set<Request>();

        public DbSet<RequestLog> RequestLog => Set<RequestLog>();

        public DbSet<Permission> Permission => Set<Permission>();
        public DbSet<Function> Function => Set<Function>();

        public DbSet<Form> Form => Set<Form>();
        public DbSet<FormInput> FormInput => Set<FormInput>();
        public DbSet<FormOption> FormOption => Set<FormOption>();
        public DbSet<Role> Roles => Set<Role>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Metadata>();
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }
    }
}

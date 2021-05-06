using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using VTAworldpass.VTACore.GenericRepository.Repositories;
using VTAworldpass.VTAServices.Services.accounts.implements;
using VTAworldpass.VTAServices.Services.incomes.implments;
using VTAworldpass.VTAServices.Services.invoices.implements;
using VTAworldpass.VTAServices.Services.utilsapp.implements;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.incomes;
using VTAworldpass.VTAServices.Services.invoices;
using VTAworldpass.VTAServices.Services.utilsapp;
using VTAworldpass.VTACore;
using VTAworldpass.VTAServices.Services.Logger;
using VTAworldpass.VTAServices.Services.accounts.security;
using VTAworldpass.Business.Services.Logger.Implementations;
using VTAworldpass.VTAServices.Services.attachments;
using VTAworldpass.VTAServices.Services.attachments.implements;
using VTAworldpass.VTAServices.Services.comments;
using VTAworldpass.VTAServices.Services.comments.implements;
using VTAworldpass.VTAServices.Services.budgets;
using VTAworldpass.VTAServices.Services.budgets.implements;
using VTAworldpass.VTAServices.Services.bankreconciliation.implements;
using VTAworldpass.VTAServices.Services.bankreconciliation;

namespace VTAworldpass.App_Start
{
    public class IoConfigurations
    {
        public static Autofac.IContainer Container { get; set; }

        public static T GetInstance<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            //RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterSecurity(builder);
            RegisterControllers(builder);

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            // Interfaces
            // builder.RegisterType(typeof(Context)).As(typeof(IContext)).InstancePerLifetimeScope();
            builder.RegisterType<ManagerPasswordHash>().As<IManagerPasswordHash>().SingleInstance();
            builder.RegisterType<AccountServices>().As<IAccountServices>().SingleInstance();
            builder.RegisterType<UtilsappServices>().As<IUtilsappServices>().SingleInstance();
            builder.RegisterType<IncomeServices>().As<IIncomeServices>().SingleInstance();
            builder.RegisterType<InvoiceServices>().As<IInvoiceServices>().SingleInstance();
            builder.RegisterType<LogsServices>().As<ILogsServices>().SingleInstance();
            builder.RegisterType<AttachmentServices>().As<IAttachmentServices>().SingleInstance();
            builder.RegisterType<CommentServices>().As<ICommentServices>().SingleInstance();
            builder.RegisterType<BugetServices>().As<IBudgetServices>().SingleInstance();
            builder.RegisterType<BankReconciliationServices>().As<IBankReconciliationServices>().SingleInstance();

            //// Clasess
            builder.Register(c => new GeneralRepository()).SingleInstance();
            builder.Register(c => new UnitOfWork()).SingleInstance();

        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {

        }

        private static void RegisterSecurity(ContainerBuilder builder)
        {

        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
        }
    }
}
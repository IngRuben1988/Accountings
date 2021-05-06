using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTACore.Cores.Globales
{
    public class Enumerables
    {
        public enum Currencies
        {
            Canadian_Dollar = 1,
            Euro            = 2,
            Mexican_Peso    = 3,
            US_Dollar       = 4,
            Dominican_Republic_Peso = 5,
            Reais_Brasil    = 6
        };

        public enum BankStatementsSheets
        {
            Resumen = 1,
            Detalles = 2
        }

        public enum BankstateRsvType
        {
            RsvMember = 1,
            RsvParent = 2
        }

        public enum BankTypeClasification
        {
            BankMovement = 1,
            BankConciliation = 2,
            BankConciliationDetail = 3,
            Unknown = 4
        }

        public enum BankMovementType
        {
            Fondeo = 1,
            Transferencia_saliente = 2,
            Transferencia_entrante = 3,
            Deposito = 4,
            Retiro = 5,
            Pago = 6,
            Comisiones = 7,
            Domiciliación = 8,
            Rendimientos = 9,
            Cheque_cobrado = 10,
            Compra = 11,
            Cuota = 12,
            Traspasos = 13,
            Otros = 14
        }

        public enum Method_Exchangue_Currencies
        {
            No_method = 1,
            Self = 2,
            Currency_Exchangue_Dictionary = 3
        };

        public enum DescriptionVTA
        {
            MaxQuickDescriptionCharacters = 250,
            MaxQuickDescriptionFileCharacters = 120,
            QuickDescriptionCharacters = 128,
            MaxQuickDescriptionCharactersTooltip = 50
        };

        public enum InvoicedItemStatus
        {
            SinEstatus = 0,
            SinRevisar = 1,
            Aprobado   = 2,
            Rechazado  = 3,
            Extemporal = 4
        };

        public enum InvoiceIncomeStatus
        {
            invoicedItemStatus_sinEstatus = 0,
            invoicedItemStatus_SinRevisar = 1,
            invoicedItemStatus_Aprobado = 2,
            invoicedItemStatus_Rechazado = 3,
            invoicedItemStatus_Extemporal = 4
        }

        public enum FinancialStateReport
        {
            Balance = 1,
            AccountHistory = 2,
            MaxBalance = 3,
            AccountHistoryConciliationsIn = 4,
            AccountHistoryOnlyConciliationsIn = 5,
            MaxBalanceConciliationIn = 6,
            BalanceConciliationIn = 7,
            BAccountCash = 8
        };

        public enum BankAccountReconcilitionStatus
        {
            Sin_Estatus = -1,
            Sin_conciliar = 0,
            Completo = 1,
            Parcial = 2,
            Error = 3,
        };

        public enum BankAccountReconciliationMethod
        {
            SinConciliar = 1,
            Manual = 2,
            Sistema = 3,
        };

        public enum PaymentMethods_Bank_Report
        {
            Transfer = 11, //Wire transfer
            Deposit = 5, // Deposit
            PayPal = 25
        };

        public enum Record
        {
            Deactivated = 0,
            Activeted = 1
        };

        public enum BankAccounType
        { // Se debe modificar segun la tabla
            SinTipo = 1,
            Débito = 2,
            Credito = 3,
            Cheques = 4,
            Efectivo = 5,
            Transferencias = 6
        };

        public enum ExpenseReport
        {
            Expenses = 1,
            ExpensesConliationsIn = 2
        }

        public enum Segment
        {
            Hotel = 1,
            Proveedor = 2,
            Construccion = 3,
            Excellent = 4,
            Fundacion = 5,
            Worldpass = 6,
            Vacationtime = 7,
            Nextpropertyadvisor = 8,
            Corporativo = 9
        };

        public enum BankAccountReconciliationStatus
        {
            Sin_Estatus = -1,
            Sin_conciliar = 0,
            Completo = 1,
            Parcial = 2,
            Error = 3,
        };

        public enum Finance
        {
            Intercompany_Finance = 1
        }

        public enum ReservaWeb
        {
            Reserva_Web = 2,
            ReservaTerminal = 6,
            PaymentType = 25
        }

        public enum ReservaStatus
        {
            Active = 1,
            Cancelled = 2,
            Pending = 3,
            Quoted = 5
        }

        public enum PurchaseSaleType
        {
            New = 1,
            Renewed = 2,
            Upgrade = 3
        }

        public enum BAccountDefault
        {
            BAccount_USD_BOA = 3
        }

        public enum BankSourceData
        {
            Fondos = 5,
            Pagos = 4,
            Ingresos = 1
        }

        public enum BankOperationType
        {
            Entradas = 1,
            Salidas = 2,
            Ambos_Sentidos = 3
        }
    }
}
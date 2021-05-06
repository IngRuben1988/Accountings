using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using VTAworldpass.VTACore.Helpers;
//using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTACore.GenericRepository.Repositories
{
    public class GeneralRepository
    {
        //private readonly VTAworldpassContext _db = new VTAworldpassContext();
        private readonly vtclubdbEntities _db = new vtclubdbEntities();
        
        public GeneralRepository()
        { }

        #region Obteniendo Cuenta Contables [ACCL1, ACCL2, ACCL3, ACCL4] por Perfil

        /// <summary>
        /// Devuelve las cuentas contables del nivel 1 (TBLACCOUNTL1) base a los parámetros solicitados Perfil de VTAccountVTClub, Segmento y ACCL1
        /// </summary>
        /// <param name="idProfile"></param>
        /// <param name="idSegment"></param>
        /// <param name="idAccl1"></param>
        /// <returns>IEnumerable<tblaccountsl1></returns>
        public IEnumerable<tblaccountsl1> getAccountL1byProfileSegment(int idProfile, int idSegment, int idAccl1)
        {

            List<tblaccountsl1> lst = new List<tblaccountsl1>();

            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var r = (from accl3prof in db.tblprofaccclass3
                         from accl4 in db.tblaccountsl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl3prof.tblaccountsl3.accountl3active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                             && accl3prof.idprofileaccount == idProfile
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                             && accl4.accountl4active == Globals.activeRecord
                             && accl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                             && accl3prof.idaccountl3 == accl4.idAccountl3
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == accl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1
                             && segmentaccl4.tblaccountsl4.idAccountl4 == accl4.idAccountl4
                             && segmentaccl4.tblsegment.idsegment == idSegment
                             && segmentaccl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                         select accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1).ToList().Distinct();
                lst = r.ToList();
            }
            return lst;
        }

        public IEnumerable<tblaccountsl2> getAccountL2byidndSegment(int id, int idProfile, int idSegment, int idAccl1)
        {
            List<tblaccountsl2> lst = new List<tblaccountsl2>();
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var r = (from accl3prof in db.tblprofaccclass3
                         from accl4 in db.tblaccountsl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl3prof.tblaccountsl3.accountl3active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.idaccountl1 == id
                             && accl3prof.idprofileaccount == idProfile
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                             && accl4.accountl4active == Globals.activeRecord
                             && accl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                             && accl3prof.idaccountl3 == accl4.idAccountl3
                             && segmentaccl4.idaccountl4 == accl4.idAccountl4
                             && segmentaccl4.tblsegment.idsegment == idSegment
                             && segmentaccl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                         select accl3prof.tblaccountsl3.tblaccountsl2).Distinct().ToList();

                foreach (tblaccountsl2 model in r.ToList())
                {
                    var lsiInt = lst.Select(v => v.idaccountl2).ToList();
                    if (!lsiInt.Contains(model.idaccountl2)) lst.Add(model);
                }
            }
            return lst;
        }

        public IEnumerable<tblaccountsl3> getAccountL3byidndSegment(int id, int idProfile, int idSegment, int idAccl1)
        {   // Usado en capturas de Ingresos y Egresos
            List<tblaccountsl3> lst = new List<tblaccountsl3>();
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var r = (from accl3prof in db.tblprofaccclass3
                         from accl4 in db.tblaccountsl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl3prof.tblaccountsl3.accountl3active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.idaccountl2 == id
                             && accl3prof.idprofileaccount == idProfile
                             && accl3prof.idaccountl3 == accl4.idAccountl3
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                             && accl4.accountl4active == Globals.activeRecord
                             && accl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                             && segmentaccl4.idaccountl4 == accl4.idAccountl4
                             && segmentaccl4.tblsegment.idsegment == idSegment
                             && segmentaccl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                         select accl3prof.tblaccountsl3).ToList().Distinct();

                lst = r.ToList();
            }
            return lst;
        }

        public IEnumerable<tblaccountsl4> getAccountL4byidndSegment(int id, int idProfile, int idSegment, int idAccl1)
        {
            List<tblaccountsl4> lst = new List<tblaccountsl4>();
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                var r = (from accl3prof in db.tblprofaccclass3
                         from accl4 in db.tblaccountsl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl3prof.tblaccountsl3.accountl3active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                             && accl3prof.idprofileaccount == idProfile
                             && accl3prof.idaccountl3 == accl4.idAccountl3
                             && accl4.accountl4active == Globals.activeRecord
                             && segmentaccl4.idaccountl4 == accl4.idAccountl4
                             && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord
                             && segmentaccl4.segmentaccl4active == Globals.activeRecord
                             && accl3prof.idaccountl3 == accl4.idAccountl3
                             && segmentaccl4.tblsegment.idsegment == idSegment
                             && accl4.idAccountl3 == id
                             && accl4.accountl4active == Globals.activeRecord
                         select accl4).ToList().Distinct();
                lst = r.ToList();
            }
            return lst;
        }

        #endregion

        #region Obteniendo Cuenta Contables [ACCL1, ACCL2, ACCL3, ACCL4] por Usuario
        /// <summary>
        /// Devuelve las cuentas contables del nivel 1 (TBLACCOUNTL1) base a los parámetros solicitados usuario de VTClub, Segmento y ACCL1
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idSegment"></param>
        /// <param name="idAccl1"></param>
        /// <returns>IEnumerable<tblaccountsl1></returns>
        public IEnumerable<tblaccountsl1> getAccountL1byUserAccl4(int idUser, int idSegment, int idAccl1)
        {
            List<tblaccountsl1> lst = new List<tblaccountsl1>();

            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var r = (from accl4 in db.tbluseraccl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl4.iduser == idUser
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.accountl3active == Globals.activeRecord
                         && accl4.tblaccountsl4.accountl4active == Globals.activeRecord
                         && accl4.useraccl4active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl1 == idAccl1

                         && segmentaccl4.idaccountl4 == accl4.idaccountl4

                         && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord
                         && segmentaccl4.tblsegment.idsegment == idSegment
                         && segmentaccl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                         select accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1

                         ).ToList().Distinct();
                lst = r.ToList();
            }
            return lst;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idSegment"></param>
        /// <param name="idUser"></param>
        /// <param name="idAccl1"></param>
        /// <returns></returns>
        public IEnumerable<tblaccountsl2> getAccountL2byUserAccl4(int id, int idSegment, int idUser, int idAccl1)
        {
            List<tblaccountsl2> lst = new List<tblaccountsl2>();

            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var r = (from accl4 in db.tbluseraccl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl4.iduser == idUser
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.accountl3active == Globals.activeRecord
                         && accl4.tblaccountsl4.accountl4active == Globals.activeRecord
                         && accl4.useraccl4active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl1 == idAccl1

                         && segmentaccl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1
                         && segmentaccl4.tblaccountsl4.idAccountl4 == accl4.idaccountl4
                         && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord

                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == id
                         && segmentaccl4.tblsegment.idsegment == idSegment

                         select accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2
                         ).ToList().Distinct();

                lst = r.ToList();
                /* foreach (tblaccountsl2 model in r.ToList())
                {
                    var lsiInt = lst.Select(v => v.idaccountl2).ToList();
                    if (!lsiInt.Contains(model.idaccountl2)) lst.Add(model);
                } */
            }
            return lst;
        }

        public IEnumerable<tblaccountsl3> getAccountL3byUserAccl4(int id, int idUser, int idSegment, int idAccl1)
        {   // Usado en capturas de Ingresos y Egresos
            List<tblaccountsl3> lst = new List<tblaccountsl3>();

            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var r = (from accl4 in db.tbluseraccl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl4.iduser == idUser
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.accountl3active == Globals.activeRecord
                         && accl4.tblaccountsl4.accountl4active == Globals.activeRecord
                         && accl4.useraccl4active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl2 == id
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl1 == idAccl1

                         && segmentaccl4.idaccountl4 == accl4.idaccountl4
                         && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord

                         && segmentaccl4.tblsegment.idsegment == idSegment
                         && segmentaccl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idAccl1

                         select accl4.tblaccountsl4.tblaccountsl3
                         ).ToList().Distinct();

                lst = r.ToList();
            }
            return lst;
        }

        public IEnumerable<tblaccountsl4> getAccountL4byUserAccl4(int id, int idUser, int idSegment, int idAccl1)
        {
            List<tblaccountsl4> lst = new List<tblaccountsl4>();

            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var r = (from accl4 in db.tbluseraccl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl4.iduser == idUser
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.accountl3active == Globals.activeRecord
                         && accl4.tblaccountsl4.accountl4active == Globals.activeRecord
                         && accl4.useraccl4active == Globals.activeRecord
                         && accl4.tblaccountsl4.idAccountl3 == id

                         && segmentaccl4.idaccountl4 == accl4.idaccountl4
                         && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord

                         && segmentaccl4.tblsegment.idsegment == idSegment

                         select accl4.tblaccountsl4
                         ).ToList().Distinct();

                /*
                List<tblaccountsl4> tmp = new List<tblaccountsl4>();
                tmp = idAccl1 != 0 ? tmp = r.Where(v => v.tblaccountsl3.tblaccountsl2.tblaccountsl1.idAccountl1 == idAccl1).ToList() : r;

                foreach (tblaccountsl4 model in tmp)
                {
                    var lsiInt = lst.Select(v => v.idAccountl4).ToList();
                    if (!lsiInt.Contains(model.idAccountl4)) lst.Add(model);
                }*/
                lst = r.ToList();
            }
            return lst;
        }

        public IEnumerable<tblaccountsl3> getAccountL3byUserAccl4(int idUser, int idSegment, int idaccl1)
        {
            List<tblaccountsl3> lst = new List<tblaccountsl3>();
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                var r = (from accl4 in db.tbluseraccl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl4.iduser == idUser
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.accountl3active == Globals.activeRecord
                         && accl4.tblaccountsl4.accountl4active == Globals.activeRecord
                         && accl4.useraccl4active == Globals.activeRecord
                         && segmentaccl4.idaccountl4 == accl4.idaccountl4
                         && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord
                         && segmentaccl4.tblsegment.idsegment == idSegment
                         select accl4.tblaccountsl4.tblaccountsl3
                         ).Distinct().ToList();

                List<tblaccountsl3> tmp = new List<tblaccountsl3>();
                tmp = idaccl1 != 0 ? tmp = r.Where(v => v.tblaccountsl2.tblaccountsl1.idaccountl1 == idaccl1).ToList() : r;
                foreach (tblaccountsl3 model in tmp)
                {
                    var lsiInt = lst.Select(v => v.idaccountl3).ToList();
                    if (!lsiInt.Contains(model.idaccountl3)) lst.Add(model);
                }
            }
            return lst;
        }


        public IEnumerable<tblaccountsl3> getAccountL3bySegment(int idProfile, int idSegment, int idaccl1)
        {

            List<tblaccountsl3> lst = new List<tblaccountsl3>();
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);


                var r = (from accl3prof in db.tblprofaccclass3
                         from accl4 in db.tblaccountsl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl3prof.tblaccountsl3.accountl3active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                             && segmentaccl4.idaccountl4 == accl4.idAccountl4
                             && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord
                             && accl4.accountl4active == Globals.activeRecord
                             && accl3prof.idprofileaccount == idProfile
                             && accl3prof.idaccountl3 == accl4.idAccountl3
                             && segmentaccl4.tblsegment.idsegment == idSegment
                         select accl3prof.tblaccountsl3).Distinct().ToList();
                List<tblaccountsl3> tmp = new List<tblaccountsl3>();
                tmp = idaccl1 != 0 ? tmp = r.Where(v => v.tblaccountsl2.tblaccountsl1.idaccountl1 == idaccl1).ToList() : r;

                foreach (tblaccountsl3 model in tmp)
                {
                    var lsiInt = lst.Select(v => v.idaccountl3).ToList();
                    if (!lsiInt.Contains(model.idaccountl3)) lst.Add(model);
                }
            }
            return lst;
        }

        public IEnumerable<tblaccountsl4> getAccountL4byidndProfile(int id, int idProfile)
        {
            List<tblaccountsl4> lst = new List<tblaccountsl4>();
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                var r = (from accl3prof in db.tblprofaccclass3
                         from accl4 in db.tblaccountsl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl3prof.tblaccountsl3.accountl3active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                             && accl3prof.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                             && accl3prof.idprofileaccount == idProfile
                             && accl4.accountl4active == Globals.activeRecord
                             && segmentaccl4.tblaccountsl4.idAccountl4 == accl4.idAccountl4
                             && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord
                             && accl3prof.idaccountl3 == accl4.idAccountl3
                             && accl4.idAccountl3 == id
                             && accl4.accountl4active == Globals.activeRecord
                         select accl4).ToList();
                foreach (tblaccountsl4 model in r.ToList())
                {
                    var lsiInt = lst.Select(v => v.idAccountl4).ToList();
                    if (!lsiInt.Contains(model.idAccountl4)) lst.Add(model);
                }
            }
            return lst;
        }

        public IEnumerable<tblaccountsl4> getAccountL4byUserAccl4(int id, int idUser, int idaccl1)
        {
            List<tblaccountsl4> lst = new List<tblaccountsl4>();
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                var r = (from accl4 in db.tbluseraccl4
                         from segmentaccl4 in db.tblsegmentaccl4
                         where accl4.iduser == idUser
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.accountl3active == Globals.activeRecord
                         && accl4.tblaccountsl4.accountl4active == Globals.activeRecord
                         && accl4.useraccl4active == Globals.activeRecord
                         && accl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl1 == idaccl1
                         && segmentaccl4.tblaccountsl4.idAccountl4 == accl4.idaccountl4
                         && segmentaccl4.tblsegment.segmentactive == Globals.activeRecord
                         && segmentaccl4.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1 == idaccl1
                         && accl4.tblaccountsl4.idAccountl3 == id
                         select accl4.tblaccountsl4
                         ).Distinct().ToList();
                foreach (tblaccountsl4 model in r.ToList())
                {
                    var lsiInt = lst.Select(v => v.idAccountl4).ToList();
                    if (!lsiInt.Contains(model.idAccountl4)) lst.Add(model);
                }
            }
            return lst;
        }


        #endregion

        #region Obteniendo Incomes Advance Search


        public async Task<IEnumerable<tblincome>> gettblncomeSearchAsync(int? number, decimal? ammountIni, decimal? ammountEnd, int? company, DateTime? applicationDateIni, DateTime? applicationDateFin, DateTime? creationDateIni, DateTime? creationDateFin, int[] companies, int[] accl3)
        {
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var _query = db.tblincome.Include(y => y.tblusers).Include(y => y.tblusers1).Include(y => y.tblcompanies).Include(y => y.tblusers.tblprofilesaccounts).Include(y => y.tblincomeitem).Include(y => y.tblcurrencies).Include(y => y.tblcompanies.tblsegment);

                _query = _query.AsNoTracking();

                if (number != null)
                {
                    _query = _query.Where(t => t.incomenumber == number);
                }
                if (ammountIni != null)
                {
                    _query = _query.Where(x => x.tblincomeitem.Sum(y => y.incomeitemsubtotal) >= ammountIni);
                }
                if (ammountEnd != null)
                {
                    _query = _query.Where(x => x.tblincomeitem.Sum(y => y.incomeitemsubtotal) <= ammountEnd);
                }
                if (company > 0)
                {
                    _query = _query.Where(x => x.tblcompanies.idcompany == company && x.tblcompanies.companyactive == Globals.activeRecord);
                }
                if (applicationDateIni != null)
                {
                    _query = _query.Where(x => x.incomeapplicationdate >= applicationDateIni);
                }
                if (applicationDateFin != null)
                {
                    _query = _query.Where(x => x.incomeapplicationdate <= applicationDateFin);
                }
                if (creationDateIni != null)
                {
                    _query = _query.Where(x => x.incomecreactiondate >= creationDateIni);
                }
                if (creationDateFin != null)
                {
                    _query = _query.Where(x => x.incomecreactiondate <= creationDateFin);
                }
                if (companies.Count() > 0)
                {
                    _query = _query.Where(y => companies.Contains(y.idcompany));
                }
                else
                {
                    throw new Exception("No tiene asignados empresas para realizar búsquedas.");
                }
                return await _query.OrderByDescending(x => x.incomeapplicationdate).Take(Globals.EntityMax150PredefinedResult).ToListAsync();
            }
        }

        #endregion

        #region Obteniendo Invoices Advance Search
        public async Task<IEnumerable<tblinvoice>> gettblInvoiceSearchAsync(int? idInvoiceNumber, decimal? ammountIni, decimal? ammountEnd, int? company, DateTime? applicationDateIni, DateTime? applicationDateFin, DateTime? creationDateIni, DateTime? creationDateFin, int[] companies, int[] accl3)
        {
            //using (var db = new VTAworldpassContext())
            using (var db = new vtclubdbEntities())
            {
                var _query = db.tblinvoice.Include(y => y.tblusers).Include(y => y.tblusers1).Include(y => y.tblcompanies).Include(y => y.tblcompanies.tblsegment).Include(y => y.tblusers.tblprofilesaccounts).Include(y => y.tblinvoiceditem).Include(y => y.tblinvoiceditem).Include(y => y.tblcurrencies).Include(y => y.tblinvoiceattach);
                _query = _query.AsNoTracking();

                if (idInvoiceNumber != null)
                {
                    _query = _query.Where(t => t.invoicenumber == idInvoiceNumber);
                }
                if (ammountIni != null)
                {
                    _query = _query.Where(x => x.tblinvoiceditem.Sum(y => y.itemsubtotal) >= ammountIni);
                }
                if (ammountEnd != null)
                {
                    _query = _query.Where(x => x.tblinvoiceditem.Sum(y => y.itemsubtotal) <= ammountEnd);
                }
                if (company > 0)
                {
                    _query = _query.Where(x => x.tblcompanies.idcompany == company && x.tblcompanies.companyactive == Globals.activeRecord);
                }
                if (applicationDateIni != null)
                {
                    _query = _query.Where(x => x.invoicedate >= applicationDateIni);
                }
                if (applicationDateFin != null)
                {
                    _query = _query.Where(x => x.invoicedate <= applicationDateFin);
                }
                if (creationDateIni != null)
                {
                    _query = _query.Where(x => x.invoicecreateon >= creationDateIni);
                }
                if (creationDateFin != null)
                {
                    _query = _query.Where(x => x.invoicecreateon <= creationDateFin);
                }
                if (companies.Count() > 0)
                {
                    _query = _query.Where(y => companies.Contains(y.idcompany));
                }
                else
                {
                    throw new Exception("No tiene asignados empresas para realizar búsquedas.");
                }
                return await _query.OrderByDescending(x => x.idinvoice).Take(Globals.EntityMax150PredefinedResult).ToListAsync();
            }
        }
        #endregion

        #region Obtiene invoiceitem Report Expense Concentrated
        public async Task<List<tblinvoiceditem>> expenseConcentratedReport(int category, int Type, int Company, DateTime? applicationDateIni, DateTime? applicationDateFin, int results, int isTax, int singleExibitionPayment, int budgetType, DateTime? creationDateIni, DateTime? creationDateFin)
        {
            var byParams = false;

            using (vtclubdbEntities db = new vtclubdbEntities())
            {
                var _query = db.tblinvoiceditem.Include(y => y.tblusers).Include(y => y.tblinvoice.tblusers1).Include(y => y.tblinvoice.tblcompanies).Include(y => y.tblusers.tblprofilesaccounts).Include(y => y.tblinvoice.tblcurrencies).Include(y => y.tblSuppliers).Include(t => t.tblinvoice.tblpayment).Include(y => y.tblbugettype).Include(y => y.tblaccountsl4).Include(y => y.tblaccountsl4.tblaccountsl3).Include(y => y.tblinvoiceitemstatus).Include(y => y.tblinvoice.tblinvoiceditem);

                if (category != 0)
                {
                    _query = _query.Where(y => y.tblaccountsl4.tblaccountsl3.idaccountl3 == category);
                    byParams = true;
                }

                if (Type != 0)
                {
                    _query = _query.Where(y => y.tblaccountsl4.idAccountl4 == Type);
                    byParams = true;
                }

                if (Company != 0)
                {
                    _query = _query.Where(y => y.tblinvoice.tblcompanies.idcompany == Company);
                    byParams = true;
                }

                if (applicationDateIni != null)
                {
                    _query = _query.Where(y => y.tblinvoice.invoicedate >= applicationDateIni);
                    byParams = true;
                }

                if (applicationDateFin != null)
                {
                    _query = _query.Where(y => y.tblinvoice.invoicedate <= applicationDateFin);
                    byParams = true;
                }

                if (creationDateIni != null)
                {
                    _query = _query.Where(y => y.tblinvoice.invoicecreateon >= creationDateIni);
                    byParams = true;
                }

                if (creationDateFin != null)
                {
                    _query = _query.Where(y => y.tblinvoice.invoicecreateon <= creationDateFin);
                    byParams = true;
                }

                if (isTax != 0)
                {
                    bool tax = isTax == 1 ? true : false;

                    _query = _query.Where(y => y.ditemistax == tax);
                    byParams = true;
                }

                if (singleExibitionPayment != 0)
                {
                    bool single = singleExibitionPayment == 1 ? true : false;
                    _query = _query.Where(y => y.itemsinglepayment == single);
                    byParams = true;
                }

                if (budgetType != 0)
                {
                    _query = _query.Where(y => y.tblbugettype.idbudgettype == budgetType);
                    byParams = true;
                }

                if (byParams == true)
                {
                    _query = _query.OrderByDescending(y => y.tblinvoice.invoicedate).ThenBy(y => y.tblaccountsl4.accountl4description).ThenBy(y => y.tblaccountsl4.tblaccountsl3.accountl3description);

                    // FInally Taken the number of rsults
                    if (results != 0)
                    {
                        _query = _query.Take(results);
                    }
                    else
                    {
                        _query = _query.Take(Globals.EntityMax1000PredefinedResult);
                    }
                }
                return  await _query.ToListAsync();
            }
        }
        #endregion
    }
}
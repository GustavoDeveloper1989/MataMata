using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppWeb.Context;
using AppWeb.Models;

namespace AppWeb.Controllers
{
    public class JogosController : Controller
    {
        private MataMataContext db = new MataMataContext();

        // GET: Jogos
        public ActionResult Index(string id)
        {

            if (id == null)
            {


            }
            else
            {

                Session["id_torneio"] = id;
            }


            int ids = int.Parse(Session["id_torneio"].ToString());

            var result_existe_jogos = db.JogosModels.Where(m => m.Id_Torneio == ids).ToList();

            if (result_existe_jogos.Count > 0)
            {


                var result_existe2 = db.JogosModels.Where(m => m.Id_Torneio == ids).ToList();

                return View(result_existe2);

            }
            else
            {

                var result_existe_campeao = db.Campeoes.Where(m => m.Id_Torneio == ids).ToList();

                if (result_existe_campeao.Count > 0)
                {


                    var result_existe2 = db.Campeoes.Where(m => m.Id_Torneio == ids).SingleOrDefault<CampeoesModel>();

                    return RedirectToAction("Campeao", new { campeao = result_existe2.Campeao.ToString() });

                }
                else
                {


                    var result = db.TorneioModels.Where(m => m.Id == ids).FirstOrDefault<TorneioModel>();

                    var result_times = db.Times
                            .Select(m =>
                              new
                              {
                                  Id = m.Id,
                                  Name = m.Nome,
                                  Escudo = m.EscudoUrl
                              }).ToList();


                    for (int i = 0; i < result.Qnt_Times / 2; i++)
                    {

                        Random rnd = new Random();

                        int r = rnd.Next(result_times.Count);

                        JogosModel jogos = new JogosModel();

                        jogos.TimeUm = result_times[r].Name;
                        jogos.EscudoUm = result_times[r].Escudo;


                        result_times.RemoveAt(r);

                        int x = rnd.Next(result_times.Count);


                        jogos.TimeDois = result_times[x].Name;
                        jogos.EscudoDois = result_times[x].Escudo;


                        result_times.RemoveAt(x);

                        jogos.Id_Torneio = int.Parse(id);

                        db.JogosModels.Add(jogos);
                        db.SaveChanges();

                    }


                    var result_existe2 = db.JogosModels.Where(m => m.Id_Torneio == ids).ToList();

                    return View(result_existe2);

                }


            }

        }

        public ActionResult Campeao(string campeao)
        {

            var result = db.Times.Where(m => m.Nome == campeao).FirstOrDefault<TimeModel>();

            return View(result);

        }

        public ActionResult FinalizarRodada()
        {

            int ids = int.Parse(Session["id_torneio"].ToString());

            var result_jogos = db.JogosModels.Where(m => m.Id_Torneio == ids).ToList();

            string[] vencedores = new string[result_jogos.Count];

            if (vencedores.Length == 1)
            {

                for (int i = 0; i < result_jogos.Count; i++)
                {

                    if (result_jogos[i].GolsUm > result_jogos[i].GolsDois)
                    {

                        vencedores[i] = result_jogos[i].TimeUm;

                    }
                    else
                    {

                        vencedores[i] = result_jogos[i].TimeDois;
                    }

                }

                var itemsToDelete = db.Set<JogosModel>();
                db.JogosModels.RemoveRange(db.JogosModels.Where(c => c.Id_Torneio == ids));
                db.SaveChanges();

                CampeoesModel campeao = new CampeoesModel();
                campeao.Id_Torneio = ids;
                campeao.Campeao = vencedores[0].ToString();

                db.Campeoes.Add(campeao);
                db.SaveChanges();

                //@@@@@@@@@@@@ REDIRECIONAR PARA UMA ACTION QUE RETORNE SÓ O ESCUDO DO CAMPEAO E PRONTO @@@@@@@@@@@@@@@@@@@@@
                return RedirectToAction("Campeao", new { campeao = vencedores[0].ToString() });

            }
            else
            {

                for (int i = 0; i < result_jogos.Count; i++)
                {

                    if (result_jogos[i].GolsUm > result_jogos[i].GolsDois)
                    {

                        vencedores[i] = result_jogos[i].TimeUm;

                    }
                    else
                    {

                        vencedores[i] = result_jogos[i].TimeDois;
                    }

                }

                var itemsToDelete = db.Set<JogosModel>();
                db.JogosModels.RemoveRange(db.JogosModels.Where(c => c.Id_Torneio == ids));
                db.SaveChanges();

                //@@@@@@@@@@@@@@@@@@ CRIANDO NOVA TABELA @@@@@@@@@@@@@@@@@@@@@

                for (int p = 0; p < vencedores.Length; p += 2)
                {

                    string venc = vencedores[p];

                    string venc2 = vencedores[p + 1];

                    JogosModel jogos = new JogosModel();

                    var result1 = db.Times.Where(m => m.Nome == venc).FirstOrDefault<TimeModel>();
                    var result2 = db.Times.Where(m => m.Nome == venc2).FirstOrDefault<TimeModel>();

                    jogos.Id_Torneio = ids;
                    jogos.TimeUm = result1.Nome;
                    jogos.EscudoUm = result1.EscudoUrl;
                    jogos.TimeDois = result2.Nome;
                    jogos.EscudoDois = result2.EscudoUrl;

                    db.JogosModels.Add(jogos);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");

            }

        }

        // GET: Jogos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JogosModel jogosModel = db.JogosModels.Find(id);
            if (jogosModel == null)
            {
                return HttpNotFound();
            }
            return View(jogosModel);
        }

        // GET: Jogos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jogos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Torneio,TimeUm,EscudoUm,TimeDois,EscudoDois,GolsUm,GolsDois")] JogosModel jogosModel)
        {
            if (ModelState.IsValid)
            {
                db.JogosModels.Add(jogosModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jogosModel);
        }

        // GET: Jogos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JogosModel jogosModel = db.JogosModels.Find(id);
            if (jogosModel == null)
            {
                return HttpNotFound();
            }
            return View(jogosModel);
        }

        // POST: Jogos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Torneio,TimeUm,EscudoUm,TimeDois,EscudoDois,GolsUm,GolsDois")] JogosModel jogosModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogosModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jogosModel);
        }

        // GET: Jogos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JogosModel jogosModel = db.JogosModels.Find(id);
            if (jogosModel == null)
            {
                return HttpNotFound();
            }
            return View(jogosModel);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JogosModel jogosModel = db.JogosModels.Find(id);
            db.JogosModels.Remove(jogosModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

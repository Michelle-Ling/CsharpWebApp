using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Assignment_MVC_App.Models;
using FIT5032_Week08A.Utils;
using Microsoft.AspNet.Identity;

namespace Assignment_MVC_App.Controllers
{
    public class SessionsController : Controller
    {
        private Vitality db = new Vitality();
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Sessions
        public ActionResult Index()
        {
            Session session = new Session();
            var userID = User.Identity.GetUserId();
            var sessions = db.Sessions.Where(s => s.TrainerID == userID).ToList();
            return View(sessions);
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: Sessions/Create
        [Authorize(Roles = "Trainer")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Trainer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SessionID,Location,SessionDate,SessionTime,SessionDetails,BookedUser,TrainerID")] Session session)
        {
            session.TrainerID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(session);
        }

        // GET: Sessions/Edit/5
        [Authorize(Roles = "Trainer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Trainer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessionID,Location,SessionDate,SessionTime,SessionDetails,BookedUser, TrainerID")] Session session)
        {
            session.TrainerID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(session);
        }

        // GET: Sessions/Delete/5
        [Authorize(Roles = "Trainer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
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

        // GET: Sessions
        public ActionResult SessionsView()
        {
            var sessions = db.Sessions.Where(s => s.BookedUser == null).ToList();
            return View(sessions);
        }

        [Authorize(Roles = "User")]
        public ActionResult BookSession(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }

            Booking booking = new Booking
            {
                SessionID = session.SessionID,
                UserID = User.Identity.GetUserId()
            };

            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                session.BookedUser = booking.UserID;
                db.SaveChanges();

                // Send email notification to user
                var toEmail = User.Identity.GetUserName();
                var subject = "Booking Confirmation";
                var contents = "Your booking is confirmed";
                                //+ "at" + db.Sessions.Select(m => m.SessionTime) + "<br /> +" +
                                //db.Sessions.Select(m => m.SessionDetails);
                //EmailSender es = new EmailSender();
                //es.Send(toEmail, subject, contents);

                Send_Email(toEmail, subject, contents);
            }

            return RedirectToAction("SessionBooked");
        }

        [HttpPost]
        public ActionResult Send_Email(string toEmail, string subject, string contents)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View();
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult SessionBooked()
        {
            return View();
        }
    }
}

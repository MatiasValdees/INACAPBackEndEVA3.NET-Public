using EVA3.Models;
using EVA3.Models.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVA3.Controllers
{
    public class PlayListController : Controller
    {
        private readonly AppDbContext _context;

        public PlayListController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            ViewModelPlayList model= new ViewModelPlayList();
            model.playLists=_context.TblPlayList.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ViewModelPlayList model) {
                _context.TblPlayList.Add(model.playList);
                _context.SaveChanges();
                return RedirectToAction("Index", "PlayList");
        }


        public IActionResult Delete(int id)
        {
            var p = _context.TblPlayList.Find(id);
            if(p != null)
            {
                _context.TblPlayList.Remove(p);
                _context.SaveChanges();
                return RedirectToAction("Index", "PlayList");
            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var p = _context.TblPlayList.Find(id);
            if(p != null)
            {
                return View(p);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(PlayList p)
        {
            if(p!=null)
            {
                _context.Update(p);
                _context.SaveChanges(true);
                return RedirectToAction("Index", "Playlist");
            }
            else
            {
                ModelState.AddModelError("", "Ocurrio un Error");
                return View();
            }

        }

        [HttpGet]
        public IActionResult Read(int id,bool error=false)
        {
            if (error)
            {
                ModelState.AddModelError("", "Cancion ya agregada a la Playlist");
            }
            var existe = _context.TblPlayList.Find(id);
            if(existe != null)
            {
                var playlistWhitSongs=_context.TblRelPlayListMusic.Where(x=>x.PlayListId==existe.PlaylistId).Include(x=>x.music).ToList();
                ViewModelPlayListMusic model= new ViewModelPlayListMusic();
                model.PlayListMusic = playlistWhitSongs;
                model.playList = existe;
                return View(model);
            }
            else { return NotFound(); }
        }

        [HttpGet]
        public IActionResult AddMusic (int IdPlaylist)
        {
            var model = new ViewModelAddMusicToPlayList();
            var playlist = _context.TblPlayList.Find(IdPlaylist);
            var musics = _context.TblMusic.ToList();
            if (musics != null && playlist != null)
            {
                model.Musics = musics;
                model.PlayList = playlist;
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult AddMusicToPlaylist(int IdMusic,int Idplaylist)
        {
            var music= _context.TblMusic.Find(IdMusic);
            var playlist= _context.TblPlayList.Find(Idplaylist);
            if (playlist != null && music!=null)
            {
                var playlistMusic = new PlayListMusic
                {
                    music = music,
                    playList = playlist
                };
                try
                {
                    _context.TblRelPlayListMusic.Add(playlistMusic);
                    _context.SaveChanges();
                }
                catch
                { 
                    return RedirectToAction("Read", new {id=Idplaylist,error=true });
                }
                return RedirectToAction("Read", new{id=Idplaylist});
            }
            else
            {
                ModelState.AddModelError("", "Ocurrio un Error");
                return RedirectToAction("Read", new { id = Idplaylist });
            }

        }
        public IActionResult DeleteSong(int IdPlaylist, int IdSong) 
        {
           var id = IdPlaylist;
           var join=_context.TblRelPlayListMusic.Where(x=>x.PlayListId==IdPlaylist).Where(x=>x.MusicId==IdSong).FirstOrDefault();
            if (join != null)
            {
                _context.TblRelPlayListMusic.Remove(join);
                _context.SaveChanges();
                return RedirectToAction("Read", new { id});
            }
            else
            {
                return NotFound();
            }
        }
    }
}

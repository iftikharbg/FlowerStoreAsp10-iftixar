using FlowerStore.DAL;
using FlowerStore.Models;
using FlowerStore.Areas.Admin.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FlowerStore.Areas.Admin.ViewModel;

namespace FlowerStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;


        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(SliderImage sliderImage)
        {
            if (!ModelState.IsValid)
            {
                return View(sliderImage);
            }
            if (!sliderImage.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "File is unsupported");
                return View();
            }
            if (sliderImage.File.Length > 1024 * 1000)
            {
                ModelState.AddModelError(nameof(sliderImage.File), "File size cannot be greater than 1 mb");
                return View();
            }
            string fileName = sliderImage.File.FileName;
            string wwwRootPath = _env.WebRootPath;

            var path = Path.Combine(wwwRootPath, "img", fileName);

            FileStream stream = new FileStream(path, FileMode.Create);
            await sliderImage.File.CopyToAsync(stream);
            stream.Close();

            sliderImage.Image = fileName;

            await _context.SliderImage.AddAsync(sliderImage);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SliderView()
        {
            var sliderView = await _context.SliderImage.ToListAsync();
            return View(sliderView);
        }

        public async Task<IActionResult> Update()
        {
            var update = _context.SliderImage.ToListAsync();
            return View(update);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _context.SliderImage.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }

            string wwwRoot = _env.WebRootPath;
            string path = Path.Combine(wwwRoot, "img", delete.Image);
            Console.WriteLine(path);

            bool isDeletedFile = await FileUtils.DelteFile(path); 

            if (isDeletedFile)
            {
                _context.SliderImage.Remove(delete);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("SliderView");
        }


        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.SliderImage.FindAsync(id);

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditSliderImage(SliderImage sliderImage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var item = await _context.SliderImage.FindAsync(sliderImage.Id);
            var wwwRooute = _env.WebRootPath;
            var path = Path.Combine(wwwRooute, "img", item.Image);
           await FileUtils.DelteFile(path);
            item.Image = sliderImage.File.FileName;
            await _context.SaveChangesAsync();
            return RedirectToAction("SliderView");
        }

        public async Task<IActionResult> AddSlider()
        {
            return View();
        } 

        [HttpPost]

        public async Task<IActionResult> AddSlider(SliderItemViewModel formFiles)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SliderView");
            }
            var slider = new Slider { Title = "Iftixar", Description = "Iftixar Qaraxanov", Signature = "Imza" };
          await  _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            foreach (var item in formFiles.Files)
            {
              
                if (!item.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("File", "File is unsupported");
                    return View();
                }
                if (item.Length > 1024 * 1000)
                {
                    ModelState.AddModelError("File", "File size cannot be greater than 1 mb");
                    return View();
                }
                string fileName = item.FileName;
                string wwwRootPath = _env.WebRootPath;

                var path = Path.Combine(wwwRootPath, "img", fileName);

                FileStream stream = new FileStream(path, FileMode.Create);
                await item.CopyToAsync(stream);
                stream.Close();
               
                var sliderImage = new SliderImage { slider = slider, Image = fileName };
                sliderImage.Image = fileName;

                await _context.SliderImage.AddAsync(sliderImage);

                await _context.SaveChangesAsync();



            }

            return View();
        }
    }
}

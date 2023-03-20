using System.Net;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class GorevController : ControllerBase
{
    private readonly GorevContext _context;

    public GorevController(GorevContext context)
    {
        _context = context;
    }

        [HttpGet]
    public async Task<List<Gorev>> Get()
    {
        var gorevler =await _context.Gorevler.ToListAsync();
        return gorevler;
    }
    [HttpGet("{id}")]
    public async Task<Gorev> Get(int id){
        var gorev =await _context.Gorevler.FirstOrDefaultAsync(k=>k.Id==id);
        return gorev;
    }
    [HttpPost]
    public async Task<Gorev> Post(GorevModel gorevModel){
        var gorev = new Gorev{
            Baslik = gorevModel.Baslik,
            Aciklama = gorevModel.Aciklama,
            Tarih = gorevModel.Tarih,
            Tamamlandi = gorevModel.Tamamlandi
        };
        await _context.Gorevler.AddAsync(gorev);
        await _context.SaveChangesAsync();
        return gorev;
    }

}
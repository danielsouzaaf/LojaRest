using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LojaRest.Controllers
{
    public class VeiculoController : ApiController
    {
        // GET: api/Veiculo
        public IEnumerable<Models.Veiculo> Get()
        {
            Models.LojaDataContext dc = new Models.LojaDataContext();
            var r = from v in dc.Veiculos select v;
            return r.ToList();
        }

        // POST: api/Veiculo
        public void Post([FromBody]string value)
        {

            List<Models.Veiculo> x = JsonConvert.DeserializeObject
            <List<Models.Veiculo>>(value);
            Models.LojaDataContext dc = new Models.LojaDataContext();
            dc.Veiculos.InsertAllOnSubmit(x);
            dc.SubmitChanges();
        }

        // PUT: api/Veiculo/5
        public void Put(int id, [FromBody]string value)
        {
            Models.Veiculo x = JsonConvert.DeserializeObject
           <Models.Veiculo>(value);
            Models.LojaDataContext dc = new Models.LojaDataContext();
            Models.Veiculo vei = (from f in dc.Veiculos
                                     where f.id == id
                                     select f).Single();
            vei.Ano = x.Ano;
            vei.DataCompra = x.DataCompra;
            vei.DataVenda = x.DataVenda;
            vei.Modelo = x.Modelo;
            vei.PrecoVenda = x.PrecoVenda;
            vei.ValorCompra = x.ValorCompra;
            vei.ValorVenda = x.ValorVenda;
            dc.SubmitChanges();
        }

        // DELETE: api/Veiculo/5
        public void Delete(int id)
        {
            Models.LojaDataContext dc = new Models.LojaDataContext();
            Models.Veiculo vei = (from f in dc.Veiculos
                                     where f.id == id
                                     select f).Single();
            dc.Veiculos.DeleteOnSubmit(vei);
            dc.SubmitChanges();
        }
    }
}

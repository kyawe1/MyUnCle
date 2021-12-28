using UncleApp.Services;
using UncleApp.Context;
using Microsoft.EntityFrameworkCore;

namespace UncleApp.Services.Workers;

public class ReportWorker : IReporterSummaryService
{
    private readonly DataContext appcontext;
    public ReportWorker(DataContext dataContext)
    {
        appcontext = dataContext;
    }
    public string GetCommaSeparatedValue(int month,int year)
    {
        var arr=appcontext.orders.Where(p=>p.Created.Month==month && p.Created.Year==year).Include(p=>p.Items).ToList();
        var temp=appcontext.dumblingTypes.Select(p=>new { name=p.Name,Id=p.Id }).ToList();
        int[] variable=new int[temp.Count];
        foreach(var i in arr){
            foreach(var q in i.Items){
                for(int j=0; j<variable.Length; j++){
                if(temp.ElementAt(j).Id==q.dumblingid){
                    variable[j]+=q.numberofitems;
                }
            }
        }
    }
    return "ok";
    }
}
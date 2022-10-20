// See https://aka.ms/new-console-template for more information
using EntityFrameworkMethods;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Entity Frameworks All Methods");

NortwindContext context = new();
///  AddedAsync
//await AddedAsync(context, "Selim", "BAYINDIR");
static async Task AddedAsync(NortwindContext context, string name, string lastname,string city)
{
    Person person = new(name, lastname,city);
    await context.AddAsync(person);
    context.SaveChanges();
    Console.WriteLine("Success");
}
///  AddRangeAsync
await AddRangeAsync(context);
static async Task AddRangeAsync(NortwindContext context)
{
    List<Person> _people;
    _people = new List<Person>
{
    new Person("Selim","BAYINDIR","ISTANBUL"),
    ///  new Urun() { ProductName = "Fanta" },
    new Person("Gülce","BAYINDIR","ISTANBUL"),
    new Person("Yiğit Can","İÇ","ISTANBUL"),
    new Person("Ayşe","BIRICIK","ANKARA"),
    new Person("AK","SUNGUR","EDİRNE"),
    new Person("Tom","HAWK","Sivas"),
    new Person("Tom","Vercetti","Sivas")
};
    await context.AddRangeAsync(_people);
    context.SaveChanges();
    Console.WriteLine("Success");
}

//UptateFalse(context);
static void UptateFalse(NortwindContext context)
{
    Person person = context.People.FirstOrDefault(p => p.Id == 8);
    person.Name = "Yahya";
    person.LastName = "Uzak";
    context.SaveChanges();
    Console.WriteLine("Success");
}
//
///Change Tracker :Context üzerinden gelen verilerin takibinden sorumlu bir mekanizmadır.
//UpdateMethod(context);
static void UpdateMethod(NortwindContext context)
{
    Person person = new();
    person.Id = 10;
    person.Name = "Yuri";
    person.LastName = "BOYKA";

    context.People.Update(person);
    context.SaveChanges();
    Console.WriteLine("Success");
}

//EntityState

//await ToListAsync(context);
static async Task ToListAsync(NortwindContext context)
{
    Person person = new();
    var personList = await context.People.ToListAsync();
    //foreach (var item in personList)
    //{
    //    Console.WriteLine(item.Name);
    //}
    ///EfCore ForEach
    personList.ForEach(x =>
    {
        Console.WriteLine(x.Name);
    });
}

//await EntityState(context);
static async Task EntityState(NortwindContext context)
{
    Person person = new();
    Console.WriteLine(context.Entry(person).State);
    person.Name = "Can";
    person.LastName = "Kızıl";
    await context.AddAsync(person);
    context.SaveChanges(true);
    Console.WriteLine(context.Entry(person).State);
}

//await Remove(context);
static async Task Remove(NortwindContext context)
{
    var deletepeople = await context.People.FirstOrDefaultAsync(p => p.Id == 14);
    Console.WriteLine(deletepeople.Name + " " + deletepeople.LastName + " Silindi ..");
    context.Remove(deletepeople);
    context.SaveChanges();
}

//await RemoveRange(context);
static async Task RemoveRange(NortwindContext context)
{
    List<Person> peopleList = await context.People.Where(p => p.Id >= 1 && p.Id <= 26).ToListAsync();
    context.RemoveRange(peopleList);
    await context.SaveChangesAsync();
    Console.WriteLine("Success");
}
//SqlRaw(context);
static void SqlRaw(NortwindContext context)
{
    var person = context.People
          .FromSqlRaw("SELECT * FROM People WHERE Name='Selim'")
          .ToList();
    person.ForEach(p =>
    {
        Console.WriteLine(p.Name);
    });
}

//FromSqlDeferedExecution(context);
static void FromSqlDeferedExecution(NortwindContext context)
{
    int urunid = 3;

    var urunler2 = from item in context.People
                   where item.Id > urunid //isteğe bağlı
                   select item;

    urunid = 10; //DEFERRED EXECUTİON SONRADAN ERTELNMİŞ ÇALIŞMA
    foreach (var item in urunler2)
    {
        Console.WriteLine(item.Id + " " + item.Name);
    }
}

///IQUERYABLE Ve Ienumarable Nedir?
///IQUERYABLE :Sorgu ya Karşılık Gelir :Ef Core üzerinden yapılmış sorgunun Execute edilmemiş halini ifade eder.
///IEnumarable:Sorgunun Çalııştırılıp/Exercute edilmiş Verilerin InMemorye yüklenmiş halini ifade eder.

//ContainsMethod(context);
static void ContainsMethod(NortwindContext context)
{
    var EfContains = from person in context.People
                     where person.Name.Contains("Selim")
                     select person;
    foreach (var item in EfContains)
    {

        Console.WriteLine("Personel Id {0} Personel Adı :{1} Personel Soyadi :{2}", item.Id, item.Name, item.LastName);

    }
}

//WherMethod(context);
static void WherMethod(NortwindContext context)
{
    var person = from per in context.People
                 where per.Id > 5 && per.Name.EndsWith("m")
                 select per;
    foreach (var item in person)
    {
        Console.WriteLine(item.Name + " " + item.LastName);
    }
}

//OrderByMethod(context);

static void OrderByMethod(NortwindContext context)
{
    var orderbyfunc = context.People.OrderBy(p => p.Name).ToList();///OrderByDescending
    orderbyfunc.ForEach(p =>
    {
        Console.WriteLine(p.Name);
    });
}

//orderbyQurable(context);

static void orderbyQurable(NortwindContext context)
{
    var orderbyquarable = from p in context.People
                          where p.Id > 5
                          orderby p.Id
                          select p;
    foreach (var item in orderbyquarable)
    {
        Console.WriteLine(item.Id + " " + item.Name + " " + item.LastName);
    }
}

///Then By
///Order By üzerinden yapılan sıralama işlemini birden fazla kolonda sağlar

//OrderByThenBy(context);
static void OrderByThenBy(NortwindContext context)
{
    var orderbyquarable = context.People
        .OrderBy(p => p.Name)
        .ThenBy(p => p.Id)
        .ToList();
    foreach (var item in orderbyquarable)
    {
        Console.WriteLine(item.Id + " " + item.Name);
    }
}

//OrderByDescending
//ThenByDescending

///Tekil Veri Getiren Sorgular
///SingleAsync
///Eğer Sorgu Sonucunda Birden Fazla Geliorsa exception fırlatır
//await SingleAsync(context);
static async Task SingleAsync(NortwindContext context)
{
    var p = await context.People.SingleAsync(u => u.Id == 2); ///>=
    Console.WriteLine(p.Name);
}

//SingleOrDefault
///Eğer Sorgu Sonucunda Birden Fazla Geliyorsa exception fırlatır hiç veri gelmez ise Null Değer Fırlatır.

//await SingleOrDefault(context);
static async Task SingleOrDefault(NortwindContext context)
{
    var p = await context.People.SingleOrDefaultAsync(u => u.Id >= 9); ///>=
    Console.WriteLine(p.Name);
}

///FirstAsync
///Sorgu Sonucunda Elde edilen Verilerden İlki gelir Eğer veri gelmiyorsa Hata Fırlatır..
//await FirstAsync(context);
static async Task FirstAsync(NortwindContext context)
{
    var p = await context.People.FirstAsync(u => u.Id == 8);
    Console.WriteLine(p.Name + " " + p.LastName);
}
//////////
///
//FirstOrDefault
///Sorgu Sonucunda Elde edilen Verilerden İlki gelir Eğer veri gelmiyorsa Null döner..
//await FirstOrDefault(context);
static async Task FirstOrDefault(NortwindContext context)
{
    var p = context.People.FirstOrDefault(u => u.Id == 5);
    Console.WriteLine(p.Name);
}


///FindAsync
///Urunun Benzersiz Olan Kolonunu çağırıyoruz. Primary Key Kolonuna hızlı bir şekilde sorgulama yapmayı sağlar.

//await FindAsync(context);

static async Task FindAsync(NortwindContext context)
{
    var p = await context.People.FindAsync(5);
    Console.WriteLine(p.Name + " " + p.LastName);
}


///Composite Primary Key Durumu

///LastAsync
///Order By Kullanılmalı 

//await LastAsync(context);
///Şartı sağlayaan en son değer gelir hic veri gelmiyorsa Exception Fırlatır.
static async Task LastAsync(NortwindContext context)
{
    var p = await context.People.OrderBy(u => u.Name).LastAsync(u => u.Id > 5);
    Console.WriteLine(p.Name);
}
///LastOrDefault
//await lastOrDefault(context);
static async Task lastOrDefault(NortwindContext context)
{
    //Şartı sağlayaan en son değer gelir hic veri gelmiyorsa Null Fırlatır.

    var p = await context.People.OrderBy(u => u.Name).LastAsync(u => u.Id > 5);
    Console.WriteLine(p.Name);
}

///CountAsync
//await CountAsync(context);
static async Task CountAsync(NortwindContext context)
{
    var p = (await context.People.ToListAsync()).Count();
    var p2 = await context.People.CountAsync(); ///integer döner
    Console.WriteLine(p);
    Console.WriteLine(p2);
}

///LongCountAsync
///Oluşturulan Sorgunun Sonucunda  execute edilmesi neticesinde kaç ,adet astırım elde edileceğini sayısal olarak (long) bizlere bildiren türdür..
//await LongCountAsync(context);
static async Task LongCountAsync(NortwindContext context)
{
    var p = await context.People.LongCountAsync();
    Console.WriteLine(p);
}

///AnyAsync 
///Sorgulanan veri geliyor mu bool turunde vAR MI Yok mu    
//await AnyAsync(context);
static async Task AnyAsync(NortwindContext context)
{
    var p = await context.People.AnyAsync(p => p.Id == 14);  //fundemental
    var p2 = await context.People.Where(u => u.Name.Contains("a")).AnyAsync(); //where kullanabilirsin
    var p3 = await context.People.AnyAsync(u => u.Name.Contains("a"));//Any nin içerisinde de yazabilirsin 
    Console.WriteLine(p + " " + p2 + " " + p3);
}
///MaxAsync
///Oluşturulan Sorguda verileN kOLONDA EN YÜKSEK DEĞER
//await MaxAsync(context);
static async Task MaxAsync(NortwindContext context)
{
    var personNumber = await context.People.MaxAsync(p => p.Id);
    var personEntity = context.People.SingleOrDefault(u => u.Id == personNumber);
    Console.WriteLine(personNumber + " " + personEntity.Name + " " + personEntity.LastName);

}
///MinAsync
//await MinAsync(context);
static async Task MinAsync(NortwindContext context)
{
    var p = await context.People.MinAsync(p => p.Id);
    Console.WriteLine(p);
}
///Distinct
///Sorguda tekrar eden kayıtlar varsa tekil kayıt getirir

//await Distinct(context); //!!
static async Task Distinct(NortwindContext context)
{
    var p = await context.People.Distinct().ToListAsync();
    p.ForEach(x =>
    {
        Console.WriteLine(x.Name);
    }
    );
}

///AllAsync
///Bir Sorgu neticesinde gelen verilerin,verilen şarta uyup uymadığını kontrol eder. true,false döner
//await AllAsync(context);


static async Task AllAsync(NortwindContext context)
{
    var person = await context.People.AllAsync(p => p.Id > 10);
    var person2 = await context.People.AllAsync(u => u.Name.Contains("a"));

    Console.WriteLine(person);
}

//Sum Fonksiyonu  :Toplam
///await SumAsync(context);

static async Task SumAsync(NortwindContext context)
{
    var fiyatToplam = await context.People.SumAsync(u => u.Id);
    Console.WriteLine(fiyatToplam);
}
///AverageAsync:Aritmetik Ortalama 
///await AverageAsync(context);
static async Task AverageAsync(NortwindContext context)
{
    var fiyatToplam = await context.People.AverageAsync(u => u.Id);
    Console.WriteLine(fiyatToplam);
}
//Contains

//await Contains(context);

static async Task Contains(NortwindContext context)
{
    var person = await context.People.Where(p => p.Name.Contains("can")).ToListAsync();
    person.ForEach(p =>
    {
        Console.WriteLine(p.Name);
    });
}
//StartsWith
///await StartsWith(context);

static async Task StartsWith(NortwindContext context)
{
    var person = await context.People.Where(p => p.Name.StartsWith("c")).ToListAsync();
    person.ForEach(p =>
    {
        Console.WriteLine(p.Name);
    });
}
///await EndsWith(context);
static async Task EndsWith(NortwindContext context)
{
    var person = await context.People.Where(p => p.LastName.EndsWith("l")).ToListAsync();
    person.ForEach(p =>
    {
        Console.WriteLine(p.Name + " " + p.LastName);
    });
}

//------------------------------------------------------------------------------------------------------------------------------------------------------//
//----------------------------------------------------------SORGU SONUCU DONUSUM FONKSİYONLARI----------------------------------------------------------//
//------------------------------------------------------------------------------------------------------------------------------------------------------//

//bU fONKSİYONLAR İLE SORGU SONUCUNDA ELDE EDİLEN VERİLERİ İSTEĞİMİZ DOIĞRULTUSUNDA FARKLI TÜRLERDE GÖRÜNTELEME SAĞLATABİLİRİZ
//ToDictionary :Pek kullanılmaz vt deki verileri dictionary şeklinde getirir

//await ToDictionaryAsync(context);
static async Task ToDictionaryAsync(NortwindContext context)
{
    var urunler = await context.People.ToDictionaryAsync(context => context.Id, context => context.Name);
    Console.WriteLine(urunler);
}

/*
 *ToList ile aynı amaca hizmet etmektedir.Yani,Oluşturulan Sorguyu execute edip neticesini alırlar.
 *ToList      :Gelen Sorgu neticesini entity türünde bir koleksiyona(List<TEntity>) dönüştürmekteyken,
 *ToDictionary:Gelen Sorgu neticesini Dictionary türnden bir koleksiyona dönüştürecektir.
 */



//ToArrayAsync 
//Oluşturulan Sorguyu dizi olarak elde eder
//ToList ile muadil amaca hizmet eder.Yani Sorguyu execute eder lakin gelen sonucu entity dizisiolarak elde eder 

///await ToArrayAsync(context);

static async Task ToArrayAsync(NortwindContext context)
{
    var urunler = await context.People.ToArrayAsync();
    Console.WriteLine(urunler);
}

//SELECT
//1: Select Fonksiyonu Generate edilecek Sorgunun Çekileceği Kolonlarını Ayarlamak için Kullanılır
///await Select(context);
static async Task Select(NortwindContext context)
{
    var people = await context.People
        .Select(u => new Person
        {
            Name = u.Name,
            //LastName= u.LastName,

        }).ToListAsync();
    people.ForEach(p =>
    {
        Console.WriteLine(p.Name + " " + p.LastName);
    });
}

//2: Select Fonksiyonu Gelen Verileri Farklı Türlerde karşılamamızı sağlar
//----------------------------------------------------------------------------------------------------------
//var people = await context.People.Include(u=>u.Department)
//    .SelectMany(u => new Person
//    {
//        Name = u.Name,
//        //LastName= u.LastName,

//    }).ToListAsync();

//Group BY:Gruplama Yapmamızı sağlayan Fonksiyondur
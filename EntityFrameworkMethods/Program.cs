// See https://aka.ms/new-console-template for more information
using EntityFrameworkMethods;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Entity Frameworks All Methods");

NortwindContext context = new();
///  AddedAsync
//await AddedAsync(context, "Selim", "BAYINDIR");
static async Task AddedAsync(NortwindContext context, string name, string lastname)
{
    Person person = new(name, lastname);
    await context.AddAsync(person);
    context.SaveChanges();
    Console.WriteLine("Success");
}
///  AddRangeAsync
//await AddRangeAsync(context);
static async Task AddRangeAsync(NortwindContext context)
{
    List<Person> _people;
    _people = new List<Person>
{
    new Person("Co","Dalton"),
    ///  new Urun() { ProductName = "Fanta" },
    new Person("Co","Dalton"),
    new Person("Co","Dalton"),
    new Person("Co","Dalton"),
    new Person("Co","Dalton"),
    new Person("Co","Dalton"),
    new Person("Co","Dalton")
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

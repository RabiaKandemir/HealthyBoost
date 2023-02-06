using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthyBoost.DAL.Seed
{
    public class CategoryS
    {
        public CategoryS(HealthyDbContext context)
        {

            context.Categories.Add(new Category() { Name = "Alkollü İçkiler ve İçecekler" });//1
            context.Categories.Add(new Category() { Name = "Alkolsüz İçecekler" });//2
            context.Categories.Add(new Category() { Name = "Baklagiller" });//3
            context.Categories.Add(new Category() { Name = "Balık ve Deniz Ürünleri" });//4
            context.Categories.Add(new Category() { Name = "Bira" });//5
            context.Categories.Add(new Category() { Name = "Bitkisel Sıvıyağlar" });//6
            context.Categories.Add(new Category() { Name = "Çerez ve Çekirdekler" });//7
            context.Categories.Add(new Category() { Name = "Çorbalar" });//8
            context.Categories.Add(new Category() { Name = "Dilimlenmiş Peynir" });//9
            context.Categories.Add(new Category() { Name = "Dip Soslar ve Ezmeler" });//10
            context.Categories.Add(new Category() { Name = "Domuz Eti" });//11
            context.Categories.Add(new Category() { Name = "Dondurma ve Donmuş Tatlılar" });//12
            context.Categories.Add(new Category() { Name = "Et ve Et Ürünleri" });//13
            context.Categories.Add(new Category() { Name = "Fast Food" });//14
            context.Categories.Add(new Category() { Name = "Geyik ve Av Etleri" });//15
            context.Categories.Add(new Category() { Name = "Kek ve Tartlar" });//16
            context.Categories.Add(new Category() { Name = "Konserve Meyveler" });//17
            context.Categories.Add(new Category() { Name = "Krem Peynir" });//18
            context.Categories.Add(new Category() { Name = "Kümes Hayvanları" });//19
            context.Categories.Add(new Category() { Name = "Makarna ve Noodle" });//20
            context.Categories.Add(new Category() { Name = "Meyve Suları" });//21
            context.Categories.Add(new Category() { Name = "Meyveler" });//22
            context.Categories.Add(new Category() { Name = "Otlar ve Baharatlar" });//23
            context.Categories.Add(new Category() { Name = "Pasta Malzemeleri" });//24
            context.Categories.Add(new Category() { Name = "Pastalar, Ekmek ve Unlu Mamuller" });//25
            context.Categories.Add(new Category() { Name = "Patates Ürünleri" });//26
            context.Categories.Add(new Category() { Name = "Peynir" });//27
            context.Categories.Add(new Category() { Name = "Pizza" });//28
            context.Categories.Add(new Category() { Name = "Sakatat ve İç Organları" });//29
            context.Categories.Add(new Category() { Name = "Şarap" });//30
            context.Categories.Add(new Category() { Name = "Sebzeler" });//31
            context.Categories.Add(new Category() { Name = "Şekerleme ve Tatlılar" });//32
            context.Categories.Add(new Category() { Name = "Sığır ve Dana Eti" });//33
            context.Categories.Add(new Category() { Name = "Sıvı ve Katı Yağlar" });//34
            context.Categories.Add(new Category() { Name = "Soda ve Meşrubatlar" });//35
            context.Categories.Add(new Category() { Name = "Söğüş Et ve Şarküteri Ürünleri" });//36
            context.Categories.Add(new Category() { Name = "Sosis ve Sucuk" });//37
            context.Categories.Add(new Category() { Name = "Soslar ve Salata Sosları" });//38
            context.Categories.Add(new Category() { Name = "Süt ve Süt Ürünleri" });//39
            context.Categories.Add(new Category() { Name = "Tahıllar ve Tahıllı Ürünler" });//40
            context.Categories.Add(new Category() { Name = "Tropik ve Egzotik Meyveler" });//41
            context.Categories.Add(new Category() { Name = "Yemekler ve Öğünler" });//42
            context.Categories.Add(new Category() { Name = "Yoğurt" });//43
            context.Categories.Add(new Category() { Name = "Yulaf Ezmesi, Müsli ve Tahıl Gevrekleri" });//44

            context.SaveChanges();
        }
    }
}

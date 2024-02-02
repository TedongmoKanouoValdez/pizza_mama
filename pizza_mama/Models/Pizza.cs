using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace pizza_mama.Models
{
    public class Pizza
    {
        [JsonIgnore]
        public int PizzaId { get; set; }

        [Display(Name = "Nom")]
        public string nom{ get; set; }

        [Display(Name = "Prix(€)")]
        public float prix { get; set; }

        //l'affichage des differents champs dans le tableau
        [Display(Name = "Vegétarienne")]
        
        public bool vegetarienne {  get; set; }

        [JsonIgnore]
        public string ingredients { get; set; }

        [NotMapped]
        //pour que entity ne stocke pas ses données dans la base de données
        [JsonPropertyName("ingredients")]
        //permet de retourner les données des ingredients sous forme de tableau dans le fichier Json
        public string[] listeingredients
        {
            get
            {
                if((ingredients == null) || (ingredients.Count() == 0))
                {
                    return null;
                }
                //elle permet de delimiter les donnes avec la virgule
                return ingredients.Split(',');
            }
        }
        
    }
}

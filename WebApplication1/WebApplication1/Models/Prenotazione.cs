﻿using static WebApplication1.Models.Clienti;

namespace WebApplication1.Models
{
    public class Prenotazione
    {
        public int PrenotazioneId { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int CameraId { get; set; }
        public Camera? Camera { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public string? Stato { get; set; }
    }
}

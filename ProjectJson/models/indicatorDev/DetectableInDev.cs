namespace ProjectWebApi.Models
{
    public class DetectableInDev
    {
        public int qtyTotal { get; set; }
        public int qtyDetectableInDev { get; set; }
        public double percentDetectableInDev { get; set; }
        public double percentReference { get; set; }
        public double qtyReference { get; set; }
    }
}
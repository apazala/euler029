

internal class Program
{
    static int[] primes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
    private static void Main(string[] args)
    {
        const int limit = 100;

        HashSet<BNum> solutionSet = new HashSet<BNum>();
        for(int a = 2; a <= limit; a++){
            BNum aNum = new BNum(a);
            for(int b = 2; b <= limit; b++){
                solutionSet.Add(aNum.Pow(b));
            }
        }

        Console.WriteLine(solutionSet.Count);

    }

    internal class BNum
    {
        int[] coeffs;
        int hash = 0;

        public BNum(int n){
            coeffs = new int[primes.Length];
            for(int i = 0; n>1 && i < primes.Length; i++){
                int p = primes[i];
                int count = 0;
                int div = p;
                while(n%div==0){
                    count++;
                    div*=p;
                }
                n = n*p/div;
                coeffs[i] = count;
            }
        }

        BNum(int[] coeffs){
            this.coeffs = (int[])coeffs.Clone();
            this.hash = 0;
        }

        public BNum Pow(int n){
            int[] poweredCoeffs = new int[coeffs.Length];
            for(int i = 0; i < poweredCoeffs.Length; i++){
                poweredCoeffs[i]=coeffs[i]*n;
            }
            return new BNum(poweredCoeffs);
        }

        public override bool Equals(object? obj)
        {
            if(!(obj is BNum num)) return false;
            for(int i = 0; i < coeffs.Length; i++){
                if(num.coeffs[i] != coeffs[i]) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            if(hash != 0) return hash;
            int h = 0;
            for (int i = 0; i < coeffs.Length; i++)
            {
                h = 31 * h + coeffs[i];
            }
            hash = h;
            return h;
        }
    }
}
namespace ObserverStrategy
{
    public class WeatherData : Subject {
        private List<Observer> observers;                       // Ricardo Valadez Leal 
        private float temperature;                              // 19211744
        private float humidity;
        private float pressure;

        public WeatherData(){
            observers = new List<Observer>();
        }

        public void registerObserver(Observer o){
            observers.Add(o);
        }

        public void removeObserver(Observer o){
            int i = observers.IndexOf(o);
            if(i >= 0){
                observers.RemoveAt(i);
            }
        }

        public void notifyObservers(){
            foreach(Observer observer in observers){
                observer.update(temperature, humidity, pressure);
            }
        }

        public void measurementsChanged(){
            notifyObservers();
        }

        public void setMeasurements(float temperature, float humidity, float pressure){
            this.temperature = temperature;
            this.humidity = humidity;
            this.pressure = pressure;
            measurementsChanged();
        }
    }

    public interface Subject{
        void registerObserver(Observer o);
        void removeObserver(Observer o);
        void notifyObservers();
    }

    public interface Observer{
        void update(float temp, float humidity, float pressure);
    }

    public interface DisplayElement{                                    // Ricardo Valadez Leal
        void display();                                                 // 19211744
    }

    public class CurrentConditionsDisplay : Observer, DisplayElement{
        private float temperature;
        private float humidity;
        private Subject weatherData;

        public CurrentConditionsDisplay(Subject weatherData){                           // Ricardo Valadez Leal
            this.weatherData = weatherData;                                             // 19211744
            weatherData.registerObserver(this);
        }

        public void update(float temperature, float humidity, float pressure){
            this.temperature = temperature;
            this.humidity = humidity;
            display();
        }

        public void display(){
            Console.WriteLine("Current conditions: " + temperature + "F degrees and " + humidity + "% humidity");
        }
    }

    public class StadisticDisplay : Observer, DisplayElement{
        private float temperature;
        private float humidity;
        private Subject weatherData;

        public StadisticDisplay(Subject weatherData){
            this.weatherData = weatherData;
            weatherData.registerObserver(this);
        }

        public void update(float temperature, float humidity, float pressure){
            this.temperature = temperature;
            this.humidity = humidity;
            display();
        }

        public void display(){
            Console.WriteLine("Stadistic conditions: " + temperature + "F degrees and " + humidity + "% humidity");
        }
    }

    public class ForecastDisplay : Observer,  DisplayElement{
        private float temperature;
        private float humidity;
        private Subject weatherData;

        public ForecastDisplay(Subject weatherData){
            this.weatherData = weatherData;
            weatherData.registerObserver(this);
        }

        public void update(float temperature, float humidity, float pressure){
            this.temperature = temperature;
            this.humidity = humidity;
            display();
        }

        public void display(){
            Console.WriteLine("Forecast conditions: " + temperature + "F degrees and " + humidity + "% humidity");
        }
    }
  
    public class WeatherStation
    {
        static void Main(string[] args)
        {
            WeatherData weatherData = new WeatherData();

            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherData);
            StadisticDisplay stadisticDisplay = new StadisticDisplay(weatherData);
            ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);

            Console.WriteLine("Weather Station");

            weatherData.setMeasurements(80, 65, 30.4f); 
            Console.WriteLine("--------------------------------------------------");

            // I want to remove an observer
            weatherData.removeObserver(stadisticDisplay);
            weatherData.removeObserver(forecastDisplay);

            weatherData.setMeasurements(82, 70, 29.2f);
            Console.WriteLine("--------------------------------------------------");

            // I want to add an observer 
            weatherData.registerObserver(stadisticDisplay);
            weatherData.setMeasurements(78, 90, 29.2f);

        }
    }
}

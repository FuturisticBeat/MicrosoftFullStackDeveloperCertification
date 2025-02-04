/* Singleton Pattern */
class Settings {

    constructor() {
        if(Settings.instance) {
            return Settings.instance;
        }
        this.configuration = {};
        Settings.instance = this;
    }

    set(key, value) {
        this.configuration[key] = value;
    }

    get(key) {
        return this.configuration[key];
    }
}

/* Module Pattern */
const CalculatorModule = (function(){
    function add(numA, numB) {
        return numA + numB;
    }

    function subtract(numA, numB) {
        return numA - numB;
    }

    return {
        add,
        subtract
    };
})();

/* Observer Pattern */
class Subject {
    constructor() {
        this.observers = [];
    }

    subscribe(observer) {
        this.observers.push(observer);
    }

    unsubscribe(observer) {
        this.observers = this.observers.filter(obs => obs != observer);
    }

    notify() {
        this.observers.forEach(observer => observer.update());
    }
}

class Observer {
    constructor(name) {
        this.name = name;
    }

    update() {
        console.log(`${this.name} received notification!`);
    }
}

const settings = new Settings();
const otherSettings = new Settings();
console.log(settings === otherSettings); // true
console.log(CalculatorModule.add(2, 8));
const observerA = new Observer("A");
const observerB = new Observer("B");
const subject = new Subject();
subject.subscribe(observerA);
subject.subscribe(observerB);
subject.notify();

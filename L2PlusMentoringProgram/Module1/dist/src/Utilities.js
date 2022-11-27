export const Utilities = {
    StopWatch: null,
    GetRandomInputObj: null
};

Utilities.StopWatch = function(methodName) {
    return {
        time: null,
        methodName: methodName,
        start: function(){
            this.time = performance.now();
            return this;
        },
        stopAndShow: function(){
            let now = performance.now();
            console.log(`Call to %c${this.methodName} %ctook %c${(now - this.time).toFixed(10)} %cmilliseconds.`, "color:blue", "", "color:red", "");
        }
    };
};

Utilities.GetRandomInputObj = function(properties) {
    let makeid = (length) => {
        var result = '';
        var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var charactersLength = characters.length;
        for ( var i = 0; i < length; i++ ) {
          result += characters.charAt(Math.floor(Math.random() * charactersLength));
       }
       return result;
    };

    let getRandomInt = (min, max) => {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1)) + min;
    };

    var ret = {};
    properties.forEach(item => {
        switch (item.type) {
          case "string": ret[item.name] = makeid(5); break;
          case "number": ret[item.name] = getRandomInt(1000, 1000000); break;
          default: throw new Error("Unsupported type error.");
        }
    });

    return ret;
};
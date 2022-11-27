function programFunction(names) {
    return {
        names,
        counter: 0,
        next: function() {

            if (this.counter >= this.names.length) {
                console.log("Program ended.");
                return;
            }

            let id = "#SectionId";
            $(id).val(this.names[this.counter]);
            $(id).trigger("change");

            this.counter++;
        }
    };
}

var arr1 = [

    "Common.WebApi-WF",
    "Common.Countries-WF",
    "Common.DeviceDetection-WF",
    "Common-WF",
    "Authentication-WF",
    "CustomerManagement-WF",
    "Logging-WF",
    "Sportsbook-WF",
    "SportsbookDataDelivery-WF",
    "SportsbookGames-WF",
    "Gaming-WF",
    "SingleSignOn-WF",
    "Content-Sportsbook-WF",
    "SportsbookCouchbase-WF",
    "IsaEnpointConfig-WF",
    "SportsbookTopicSubscription-WF",
    "UkCustomerManagement-WF",
    "Caching-WF",
    "CacheControl-WF",
    "Administration",
    "SportsbookBetting-WF",
    "CommonMarkets",
];
var program = programFunction(arr1);
document.addEventListener('keypress', function(e){
    if (e.key == "q") {
        console.log("next called");
        program.next();
    }
});
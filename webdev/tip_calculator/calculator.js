
//Tips start at 10% for rating 1 and increase by 1% for each consecutive higher rating so rating of 10 is 19% tip.
const tipArray = [0.1, 0.11, 0.12, 0.13, 0.14, 0.15, 0.16, 0.17, 0.18, 0.19];

function check() {

  // make sure form has valid entries in both boxes. If not reveal hidden alert that says the form isn't valid.
    var bill,rating;
    bill = document.getElementById("billAmount").value;
    rating = document.getElementById("rating").value;
    if (bill == "" || rating == "Rate your experience") {
      let element = document.getElementById("invalidAlert");
      element.removeAttribute("hidden");
        return false;

    //form is valid so calculate tip.
    }else{
      //hide user form
      var tf = document.getElementById('tipForm');
      tf.style.display = "none";

      //custom amount entered by user
      var amount = 0;

      //percent of bill to tip based off of rating
      var percent = tipArray[rating-1];

      //check if customer entered a custom tip, and if so adjust amount and percent variables
      var customPercent,customAmount;
      customPercent = document.getElementById("percentage").value;
      customAmount = document.getElementById("amount").value;

      //if custom amount entered
      if (customAmount != "") {
        amount = customAmount;
        //no percentage to tip
        percent = 0;
      }
      //if custom percent entered by user
      if (customPercent != "") {
        //the percentage changes to the user specified one
        percent = customPercent/100;
      }
      //if user enters both a custom percentage and a custom amount the tip is those two added together.

      //full tip is the percentage of bill specified plus custom amount
      //All $ are set to show 2 decimal places
      var tip = (parseFloat(percent*bill)+parseFloat(amount)).toFixed(2);

      //place variables in specified html element.
      document.getElementById("meal").innerHTML = (parseFloat(bill)).toFixed(2);
      document.getElementById("quality").innerHTML = rating;
      document.getElementById("tip").innerHTML = tip;
      document.getElementById("total").innerHTML = (parseFloat(bill) + parseFloat(tip)).toFixed(2);

      let element = document.getElementById("tipResult");

      //reveal the hidden html result block
      element.removeAttribute("hidden");

    }
}

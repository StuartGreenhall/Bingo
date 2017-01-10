// Write your Javascript code.

$( document ).ready(function() {
    var rawData = $('#analytics').attr('data-attr');
    var results = JSON.parse(rawData);

    var distribution = document.getElementById("distribution").getContext("2d");
    distribution.canvas.width = 300;
    distribution.canvas.height = 300;
    var myChart = new Chart(distribution, {
        type: 'doughnut',
        data: {
            labels: [
                "Natural",
                "Ad",
                "Complimentory"
            ],
            datasets: [
                {
                    data: results.ResultTypeDistribution,
                    backgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56"
                    ],
                    hoverBackgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56"
                    ]
                }
            ]
        }, 
        options: {
            title: {
                display: true,
                text: 'Result Type Distribution'
            }
        }
    });
    // var myChart = new Chart(distribution, {
    //     type: 'bar',
    //     data: {
    //         labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
    //         datasets: [{
    //             label: '# of Votes',
    //             data: [12, 19, 3, 5, 2, 3],
    //             backgroundColor: [
    //                 'rgba(255, 99, 132, 0.2)',
    //                 'rgba(54, 162, 235, 0.2)',
    //                 'rgba(255, 206, 86, 0.2)',
    //                 'rgba(75, 192, 192, 0.2)',
    //                 'rgba(153, 102, 255, 0.2)',
    //                 'rgba(255, 159, 64, 0.2)'
    //             ],
    //             borderColor: [
    //                 'rgba(255,99,132,1)',
    //                 'rgba(54, 162, 235, 1)',
    //                 'rgba(255, 206, 86, 1)',
    //                 'rgba(75, 192, 192, 1)',
    //                 'rgba(153, 102, 255, 1)',
    //                 'rgba(255, 159, 64, 1)'
    //             ],
    //             borderWidth: 1
    //         }]
    //     }
});

$('#analyse').on('click', function() {
    $('#search-results').fadeOut();
    $('#analytics').fadeIn();
});
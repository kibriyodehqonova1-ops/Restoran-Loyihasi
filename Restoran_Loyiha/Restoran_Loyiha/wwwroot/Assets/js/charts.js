let ctx = document.getElementById('ordersChart');
new Chart(ctx, {
    type: 'line',
    data: {
        labels: ["Dush", "Sesh", "Chor", "Pay", "Juma", "Shan", "Yak"],
        datasets: [{
            label: "Buyurtmalar",
            data: [40, 55, 60, 43, 90, 67, 80]
        }]
    }
});

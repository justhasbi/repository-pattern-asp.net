$.ajax({
    url: 'https://pokeapi.co/api/v2/pokemon/'
}).done(res => {
    let htmlContent = "";
    $.each(res.results, (key, value) => {
        htmlContent += `<tr>
                            <td>${key + 1}</td>
                            <td>${value.name}</td>
                            <td>${value.url}</td>
                            <td>
                                <button
                                    type="button"
                                    class="item-detail btn btn-primary"
                                    data-toggle="modal"
                                    data-target="#pokemonDetailModal"
                                    onClick="pokemonDetail('${value.url}')">Detail</button>
                                <button class="btn btn-danger">Hapus</button>
                            </td>
                        </tr>`
    });

    $('.data-pokemon').html(htmlContent)
})

const pokemonDetail = (url) => {
    $.ajax({
        url: url
    }).done(res => {
        console.log(res)

        let abilities = "";
        let types = ""

        res.abilities.map(item => {
            abilities += `<li>${item.ability.name}</li>`
        })

        res.types.map(item => console.log(item.type.name))

        res.types.map(item => {
            if (item.type.name === "grass") {
                types += `<span class="badge badge-success mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "poison") {
                types += `<span class="badge badge-danger mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "fire") {
                types += `<span class="badge badge-warning mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "flying") {
                types += `<span class="badge badge-info mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "normal") {
                types += `<span class="badge badge-light mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "water") {
                types += `<span class="badge badge-primary mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "bug") {
                types += `<span class="badge badge-success mr-1">${item.type.name}</span>`
            }
        })


        let pokeDetailContent = `

                                <img
                                    src="${res.sprites.other.dream_world.front_default}"
                                    class="mx-auto d-block mb-3"
                                    alt="pokeimg"
                                    width="350px"
                                    height="350px" />
                                    <div class="text-center">${types}</div>
                                <ul style="list-style:none; font-size: 1.2em;">
                                    <li><b>Name :</b> ${res.name} </li>
                                    <li><b>Weight :</b> ${res.height} </li>
                                    <li><b>Height :</b> ${res.weight}</li>
                                    <li><b>Abilities :</b>
                                        <ul>
                                            ${abilities}
                                        </ul>
                                    </li>
                                </ul>`;

        $('#pokemonDetailModal .modal-body').html(pokeDetailContent);
        $('h5.modal-title').html(`${res.name}`.toUpperCase());
    });
}
$(document).ready(() => {
    $.ajax({
        url: 'https://swapi.dev/api/people',
    }).done(result => {
        let items = "";
        $.each(result.results, (key, val) => {
            items += `<tr class="item${key + 1}">
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td>
                        <button class="item-detail btn btn-primary"
                                data-toggle="modal"
                                data-target="#exampleModalLong"
                                onClick="detailItem('${val.url}')">Detail</button>
                        <button class="btn btn-danger" onClick="removeItem('item${key + 1}')">Hapus</button>
                    </td>
                  </tr>`
        });
        $('.items').html(items);
    });
});

const removeItem = (id) => {
    $(`tr.${id}`).remove();
}

const detailItem = (url) => {
    $.ajax({
        url: url
    }).done(result => {
        let detailText = `<ul>
                            <li>Name: ${result.name}</li>
                            <li>Gender: ${result.gender}</li>
                            <li>Height: ${result.height}</li>
                            <li>Birth Year: ${result.birth_year}</li>
                          </ul>`;
        $('.modal-body').html(detailText);
        $('h5.modal-title').html(`Character Detail: ${result.name}`);
    }).fail(result => {
        console.log(result);
    });
}

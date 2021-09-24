

let dataTable = $('#dataTable').DataTable({
    "filter": true,
    "ajax": {
        "url": "person/getallpersons",
        "datatype": "json",
        "dataSrc": ""
    },
    "dom": 'Bfrtip',
    "buttons": [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: [1, 2, 3, 4, 5, 6]
            },
            bom: true
        },
        {
            extend: 'pdfHtml5',
            exportOptions: {
                columns: [1, 2, 3, 4, 5, 6]
            },
            bom: true
        },
        {
            extend: 'csvHtml5',
            exportOptions: {
                columns: [1, 2, 3, 4, 5, 6]
            },
            bom: true
        },
        {
            extend: 'print',
            exportOptions: {
                columns: [1, 2, 3, 4, 5, 6]
            },
            bom: true
        },
    ],
    "columns": [
        {
            "data": "id",
            "render": function (data, type, row, meta) {
                return meta.row + meta.settings._iDisplayStart + 1;
            },
            "orderable": false
        },
        {
            "data": "nik"
        },
        {
            "data": "fullName",
            "autoWidth": true
        },
        {
            "data": "email",
            "autoWidth": true
        },
        {
            "data": "gender",
            "autoWidth": true
        },
        {
            "data": "phone",
            "render": function (data, type, row) {
                if (row['phone'].startsWith('0')) {
                    return `+62${row['phone'].substr(1)}`
                }
                return `+62${row['phone']}`
            },
            "autoWidth": true
        },
        {
            "data": "universityName",
            "autoWidth": true
        },
        {
            "data": null,
            "render": function (data, type, row) {
                const dataRow = `<button
                                        type="button"
                                        data-toggle="modal"
                                        data-target="#personDetail"
                                        onClick="person('${row["nik"]}')"
                                        class="item-detail btn btn-primary btn-sm">Detail</button>
                                 <button
                                        type="button"
                                        onClick="deletePerson('${row["nik"]}')"
                                        class="btn btn-danger btn-sm">Delete</button>`
                return dataRow;
            },
            "autoWidth": true,
            "orderable": false
        }
    ]
});

// person detail
const person = (nik) => {
    $.ajax({
        url: `person/GetSinglePerson/${nik}`,
        method: 'GET'
    }).done(res => {
        console.log(res)
        let personData = `

            <table class="table table-bordered">
                <tbody>
                    <tr>
                        <td><b>NIK :</b> ${res.nik}</td>
                    </tr>
                    <tr>
                        <td><b>Full Name :</b> ${res.fullName}</td>
                    </tr>
                    <tr>
                        <td><b>Gender :</b> ${res.gender}</td>
                    </tr>
                    <tr>
                        <td><b>Birth Date :</b> ${res.birthDate}</td>
                    </tr>
                    <tr>
                        <td><b>Phone Number :</b> ${res.phone.startsWith('0') ? '+62' + res.phone.substr(1) : '+62' + res.phone}</td>
                    </tr>
                    <tr>
                        <td><b>University :</b> ${res.universityName}</td>
                    </tr>
                    <tr>
                        <td><b>Degree :</b> ${res.degree}</td>
                    </tr>
                    <tr>
                        <td><b>GPA :</b> ${res.gpa}</td>
                    </tr>
                    <tr>
                        <td><b>Salary :</b> Rp. ${res.salary}</td>
                    </tr>
                </tbody>
            </table>`

        $('#personDetail .modal-body').html(personData);
        $('h5.modal-title').html(`${res.fullName}`.toUpperCase());
    });
};

// register
/*
$("#btnSubmit").click(e => {
    e.preventDefault()
    const nik = $('#nik').val();
    const phone = $('#phone').val();
    const firstName = $('#first').val();
    const lastName = $('#last').val();
    const birthDate = $('#birthdate').val();
    const gender = $('#gender').val();
    const salary = $('#salary').val();
    const email = $('#email').val();
    const password = $('#pass').val();
    const degree = $('#degree').val();
    const gpa = $('#gpa').val();
    const role = $('#role').val();
    const university = $('#university').val();

    const data = {
        "NIK": nik,
        "FirstName": firstName,
        "LastName": lastName,
        "Phone": phone,
        "BirthDate": birthDate,
        "Salary": salary,
        "Email": email,
        "Gender": parseInt(gender),
        "Password": password,
        "universityId": parseInt(university),
        "Degree": degree,
        "GPA": gpa,
        "RoleId": parseInt(role)
    };

    // form validation

    console.log(JSON.stringify(data));
    // post data to database
    
});
*/

const forms = document.querySelectorAll('.needs-validation');
Array.prototype.slice.call(forms).forEach((form) => {
    $('.register-form').submit((e) => {
        if (!form.checkValidity()) {
            e.preventDefault();
            e.stopPropagation();
        }
        form.classList.add('was-validated');

        e.preventDefault();

        let data = new Object();

        data.NIK = $('#nik').val();
        data.Phone = $('#phone').val();
        data.FirstName = $('#first').val();
        data.LastName = $('#last').val();
        data.BirthDate = $('#birthdate').val();
        data.Gender = parseInt($('#gender').val());
        data.Salary = $('#salary').val();
        data.Email = $('#email').val();
        data.Password = $('#pass').val();
        data.Degree = $('#degree').val();
        data.GPA = $('#gpa').val();
        data.RoleId = parseInt($('#role').val());
        data.UniversityId = parseInt($('#university').val());

        console.log(JSON.stringify(data))
        $.ajax({
            url: 'https://localhost:44350/accounts/registerdata/',
            type: 'post',
            dataType: 'json',
            //contentType: 'application/json; charset=utf-8',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: data,
            success: function (data) {
                $('#registerModal').modal('hide')
                form.reset();
                form.classList.add('needs-validation');
                swal({
                    title: "Success Insert Data",
                    icon: "success",
                }).then(res => dataTable.ajax.reload())
            },
            error: function () {
                swal({
                    title: "Error Insert Data",
                    icon: "error",
                }).then(res => dataTable.ajax.reload())
        }
        });
    });
})

// generate university select form
$.ajax({
    url: 'https://localhost:44300/api/universities',
    method: 'GET'
}).done(res => {
    let selectItem = '';

    $.each(res, (key, val) => {
        selectItem += `<option value="${val.universityId}">${val.name}</option>`
    });
    $('#university').html(selectItem);
}).fail(res => console.log(res));

// delete ajax
const deletePerson = (nik) => {
    swal({
        title: 'Apakah anda yakin menghapus data ?',
        icon: 'warning',
        buttons: ['Cancel', 'Yes!']
    }).then(result => {
        if (result) {
            $.ajax({
                url: 'person/delete/' + nik,
                method: 'DELETE',
                success: function (data) {
                    swal({
                        title: "Success Delete Data",
                        icon: "success",
                    }).then(res => dataTable.ajax.reload())
                },
            });
        }
    })
}
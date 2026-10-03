/*=========================================================================================
    File Name: kanban.js
    Description: kanban plugin
==========================================================================================*/

$(document).ready(function () {
	var kanban_curr_el, kanban_curr_item_id, kanban_item_title, kanban_data, kanban_item, kanban_users;

	// Kanban Board and Item Data passed by json
	var kanban_board_data = [{
			id: "kanban-board-1",
			title: "بازاریابی",
			item: [{
					id: "11",
					title: "کمپین فیسبوک 😎",
					border: "success",
					dueDate: "6 اردیبهشت",
					comment: 1,
					attachment: 3,
					users: [
						"admin/images/portrait/small/avatar-s-11.jpg",
						"admin/images/portrait/small/avatar-s-12.jpg"
					]
				},
				{
					id: "12",
					title: "چیزی بنویسید",
					border: "info",
					image: "admin/images/banner/banner-21.jpg",
					dueDate: "10 اردیبهشت"
				},
				{
					id: "13",
					title: "گرافیک های شبکه های اجتماعی",
					border: "warning",
					dueDate: "3 اردیبهشت",
					comment: 23,
					attachment: 4,
					users: [
						"admin/images/portrait/small/avatar-s-1.jpg",
						"admin/images/portrait/small/avatar-s-18.jpg"
					]
				},
				{
					id: "14",
					title: "لورم ایپسوم متن ساختگی با تولید سادگی",
					border: "danger",
					comment: 56,
					attachment: 2,
					users: [
						"admin/images/portrait/small/avatar-s-26.jpg",
						"admin/images/portrait/small/avatar-s-16.jpg"
					]
				},
				{
					id: "15",
					title: "بازاریابی توییتر",
					border: "secondary"
				}
			]
		},
		{
			id: "kanban-board-2",
			title: "طراحی UI",
			item: [{
					id: "21",
					title: "طراحی ابزار UI فلت",
					border: "secondary"
				},
				{
					id: "22",
					title: "لورم ایپسوم متن ساختگی با تولید",
					border: "info",
					dueDate: "1 اردیبهشت",
					comment: 8,
					users: [
						"admin/images/portrait/small/avatar-s-24.jpg",
						"admin/images/portrait/small/avatar-s-14.jpg"
					]
				},
				{
					id: "23",
					title: "طراحی برنامه",
					border: "warning"
				},
				{
					id: "24",
					title: "طراحی لوگو 😱",
					border: "primary",
					dueDate: "6 اردیبهشت",
					comment: 10,
					attachment: 6,
					badgeContent: "آ‌ک",
					badgeColor: "danger"
				}
			]
		},
		{
			id: "kanban-board-3",
			title: "توسعه نرم افزار",
			item: [{
					id: "31",
					title: "لورم ایپسوم متن ساختگی با تولید سادگی نا مفهوم",
					border: "warning",
					dueDate: "1 خرداد",
					comment: 10,
					users: [
						"admin/images/portrait/small/avatar-s-20.jpg",
						"admin/images/portrait/small/avatar-s-22.jpg",
						"admin/images/portrait/small/avatar-s-13.jpg"
					]
				},
				{
					id: "32",
					title: "داشبورد ادمین 🙂",
					border: "success",
					dueDate: "6 خرداد",
					comment: 7,
					badgeContent: "آد",
					badgeColor: "primary"
				},
				{
					id: "33",
					title: "لورم ایپسوم متن ساختگی با تولید سادگی",
					border: "primary",
					dueDate: "9 خرداد",
					users: [
						"admin/images/portrait/small/avatar-s-1.jpg",
						"admin/images/portrait/small/avatar-s-2.jpg"
					]
				}
			]
		}
	];

	// Kanban Board
	var KanbanExample = new jKanban({
		element: "#kanban-wrapper", // selector of the kanban container
		buttonContent: "+ افزودن آیتم جدید", // text or html content of the board button

		// click on current kanban-item
		click: function (el) {
			// kanban-overlay and sidebar display block on click of kanban-item
			$(".kanban-overlay").addClass("show");
			$(".kanban-sidebar").addClass("show");

			// Set el to var kanban_curr_el, use this variable when updating title
			kanban_curr_el = el;

			// Extract  the kan ban item & id and set it to respective vars
			kanban_item_title = $(el).contents()[0].data;
			kanban_curr_item_id = $(el).attr("data-eid");

			// set edit title
			$(".edit-kanban-item .edit-kanban-item-title").val(kanban_item_title);
		},

		buttonClick: function (el, boardId) {
			// create a form to add add new element
			var formItem = document.createElement("form");
			formItem.setAttribute("class", "itemform");
			formItem.innerHTML =
				'<div class="form-group">' +
				'<textarea class="form-control add-new-item" rows="2" autofocus required></textarea>' +
				"</div>" +
				'<div class="form-group">' +
				'<button type="submit" class="btn btn-primary btn-sm mr-50">ثبت</button>' +
				'<button type="button" id="CancelBtn" class="btn btn-sm btn-danger">انصراف</button>' +
				"</div>";

			// add new item on submit click
			KanbanExample.addForm(boardId, formItem);
			formItem.addEventListener("submit", function (e) {
				e.preventDefault();
				var text = e.target[0].value;
				KanbanExample.addElement(boardId, {
					title: text
				});
				formItem.parentNode.removeChild(formItem);
			});
			$(document).on("click", "#CancelBtn", function () {
				$(this).closest(formItem).remove();
			})
		},
		addItemButton: true, // add a button to board for easy item creation
		boards: kanban_board_data // data passed from defined variable
	});

	// Add html for Custom Data-attribute to Kanban item
	var board_item_id, board_item_el;
	// Kanban board loop
	for (kanban_data in kanban_board_data) {
		// Kanban board items loop
		for (kanban_item in kanban_board_data[kanban_data].item) {
			var board_item_details = kanban_board_data[kanban_data].item[kanban_item]; // set item details
			board_item_id = $(board_item_details).attr("id"); // set 'id' attribute of kanban-item

			(board_item_el = KanbanExample.findElement(board_item_id)), // find element of kanban-item by ID
			(board_item_users = board_item_dueDate = board_item_comment = board_item_attachment = board_item_image = board_item_badge =
				" ");

			// check if users are defined or not and loop it for getting value from user's array
			if (typeof $(board_item_el).attr("data-users") !== "undefined") {
				for (kanban_users in kanban_board_data[kanban_data].item[kanban_item].users) {
					board_item_users +=
						'<li class="avatar pull-up my-0">' +
						'<img class="media-object rounded-circle" src=" ' +
						kanban_board_data[kanban_data].item[kanban_item].users[kanban_users] +
						'" alt="Avatar" height="24" width="24">' +
						"</li>";
				}
			}
			// check if dueDate is defined or not
			if (typeof $(board_item_el).attr("data-dueDate") !== "undefined") {
				board_item_dueDate =
					'<div class="kanban-due-date d-flex align-items-center mr-50">' +
					'<i class="bx bx-time-five font-size-small mr-25"></i>' +
					'<span class="font-size-small">' +
					$(board_item_el).attr("data-dueDate") +
					"</span>" +
					"</div>";
			}
			// check if comment is defined or not
			if (typeof $(board_item_el).attr("data-comment") !== "undefined") {
				board_item_comment =
					'<div class="kanban-comment d-flex align-items-center mr-50">' +
					'<i class="bx bx-message font-size-small mr-25"></i>' +
					'<span class="font-size-small">' +
					$(board_item_el).attr("data-comment") +
					"</span>" +
					"</div>";
			}
			// check if attachment is defined or not
			if (typeof $(board_item_el).attr("data-attachment") !== "undefined") {
				board_item_attachment =
					'<div class="kanban-attachment d-flex align-items-center">' +
					'<i class="bx bx-link-alt font-size-small mr-25"></i>' +
					'<span class="font-size-small">' +
					$(board_item_el).attr("data-attachment") +
					"</span>" +
					"</div>";
			}
			// check if Image is defined or not
			if (typeof $(board_item_el).attr("data-image") !== "undefined") {
				board_item_image =
					'<div class="kanban-image mb-1">' +
					'<img class="img-fluid" src=" ' +
					kanban_board_data[kanban_data].item[kanban_item].image +
					'" alt="kanban-image">';
				("</div>");
			}
			// check if Badge is defined or not
			if (typeof $(board_item_el).attr("data-badgeContent") !== "undefined") {
				board_item_badge =
					'<div class="kanban-badge">' +
					'<div class="badge-circle badge-circle-sm badge-circle-light-' +
					kanban_board_data[kanban_data].item[kanban_item].badgeColor +
					' font-size-small font-weight-bold">' +
					kanban_board_data[kanban_data].item[kanban_item].badgeContent +
					"</div>";
				("</div>");
			}
			// add custom 'kanban-footer'
			if (
				typeof (
					$(board_item_el).attr("data-dueDate") ||
					$(board_item_el).attr("data-comment") ||
					$(board_item_el).attr("data-users") ||
					$(board_item_el).attr("data-attachment")
				) !== "undefined"
			) {
				$(board_item_el).append(
					'<div class="kanban-footer d-flex justify-content-between mt-1">' +
					'<div class="kanban-footer-left d-flex">' +
					board_item_dueDate +
					board_item_comment +
					board_item_attachment +
					"</div>" +
					'<div class="kanban-footer-right">' +
					'<div class="kanban-users">' +
					board_item_badge +
					'<ul class="list-unstyled users-list m-0 d-flex align-items-center">' +
					board_item_users +
					"</ul>" +
					"</div>" +
					"</div>" +
					"</div>"
				);
			}
			// add Image prepend to 'kanban-Item'
			if (typeof $(board_item_el).attr("data-image") !== "undefined") {
				$(board_item_el).prepend(board_item_image);
			}
		}
	}

	// Add new kanban board
	//---------------------
	var addBoardDefault = document.getElementById("add-kanban");
	var i = 1;
	addBoardDefault.addEventListener("click", function () {
		KanbanExample.addBoards([{
			id: "kanban-" + i, // generate random id for each new kanban
			title: "عنوان پیش فرض"
		}]);
		var kanbanNewBoard = KanbanExample.findBoard("kanban-" + i)

		if (kanbanNewBoard) {
			$(".kanban-title-board").on("mouseenter", function () {
				$(this).attr("contenteditable", "true");
				$(this).addClass("line-ellipsis");
			});
			kanbanNewBoardData =
				'<div class="dropdown">' +
				'<div class="dropdown-toggle cursor-pointer" role="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="bx bx-dots-vertical-rounded"></i></div>' +
				'<div class="dropdown-menu" aria-labelledby="dropdownMenuButton"> ' +
				'<a class="dropdown-item" href="#"><i class="bx bx-link mr-50"></i>کپی لینک</a>' +
				'<a class="dropdown-item kanban-delete" id="kanban-delete" href="#"><i class="bx bx-trash mr-50"></i>حذف</a>' +
				"</div>" + "</div>";
			var kanbanNewDropdown = $(kanbanNewBoard).find("header");
			$(kanbanNewDropdown).append(kanbanNewBoardData);
		}
		i++;

	});

	// Delete kanban board
	//---------------------
	$(document).on("click", ".kanban-delete", function (e) {
		var $id = $(this)
			.closest(".kanban-board")
			.attr("data-id");
		addEventListener("click", function () {
			KanbanExample.removeBoard($id);
		});
	});

	// Kanban board dropdown
	// ---------------------

	var kanban_dropdown = document.createElement("div");
	kanban_dropdown.setAttribute("class", "dropdown");

	dropdown();

	function dropdown() {
		kanban_dropdown.innerHTML =
			'<div class="dropdown-toggle cursor-pointer" role="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="bx bx-dots-vertical-rounded"></i></div>' +
			'<div class="dropdown-menu" aria-labelledby="dropdownMenuButton"> ' +
			'<a class="dropdown-item" href="#"><i class="bx bx-link-alt mr-50"></i>کپی لینک</a>' +
			'<a class="dropdown-item kanban-delete" id="kanban-delete" href="#"><i class="bx bx-trash mr-50"></i>حذف</a>' +
			"</div>";
		if (!$(".kanban-board-header div").hasClass("dropdown")) {
			$(".kanban-board-header").append(kanban_dropdown);
		}
	}

	// Kanban-overlay and sidebar hide
	// --------------------------------------------
	$(
		".kanban-sidebar .delete-kanban-item, .kanban-sidebar .close-icon, .kanban-sidebar .update-kanban-item, .kanban-overlay"
	).on("click", function () {
		$(".kanban-overlay").removeClass("show");
		$(".kanban-sidebar").removeClass("show");
	});

	// Updating Data Values to Fields
	// -------------------------------
	$(".update-kanban-item").on("click", function (e) {
		// var $edit_title = $(".edit-kanban-item .edit-kanban-item-title").val();
		// $(kanban_curr_el).txt($edit_title);
		e.preventDefault();
	});

	// Delete Kanban Item
	// -------------------
	$(".delete-kanban-item").on("click", function () {
		$delete_item = kanban_curr_item_id;
		addEventListener("click", function () {
			KanbanExample.removeElement($delete_item);
		});
	});

	// Kanban Quill Editor
	// -------------------
	var composeMailEditor = new Quill(".snow-container .compose-editor", {
		modules: {
			toolbar: ".compose-quill-toolbar"
		},
		placeholder: "یک دیدگاه بنویسید ... ",
		theme: "snow"
	});

	// Making Title of Board editable
	// ------------------------------
	$(".kanban-title-board").on("mouseenter", function () {
		$(this).attr("contenteditable", "true");
		$(this).addClass("line-ellipsis");
	});

	// kanban Item - Pick-a-Date
	$(".edit-kanban-item-date").datepicker({
		dateFormat: "yy/mm/dd",
		showOtherMonths: true,
		selectOtherMonths: false
	});

	// Perfect Scrollbar - card-content on kanban-sidebar
	if ($(".kanban-sidebar .edit-kanban-item .card-content").length > 0) {
		new PerfectScrollbar(".card-content", {
			wheelPropagation: false
		});
	}

	// select default bg color as selected option
	$("select").addClass($(":selected", this).attr("class"));

	// change bg color of select form-control
	$("select").change(function () {
		$(this)
			.removeClass($(this).attr("class"))
			.addClass($(":selected", this).attr("class") + " form-control text-white");
	});
});
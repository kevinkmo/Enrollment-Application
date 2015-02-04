define(['Models/CheckMaxUnitsModel'], function (CheckMaxUnitsModel) {
    function CheckMaxUnitsViewModel() {

        var obj = new CheckMaxUnitsModel();
        var that = this;
        var initialBind = true;
        var CheckMaxUnitsViewModel = ko.observableArray();

        

        this.GetAll = function () {

            obj.GetAll(function (studentList) {
                CheckMaxUnitsViewModel.removeAll();
                console.log('studentList = ', studentList);
                for (var i = 0; i < studentList.length; i++) {
                    CheckMaxUnitsViewModel.push({
                        id: studentList[i].StudentId,
                        Total_Unit: studentList[i].Course.unit,
                   
               
                    });
                }
                console.log('list = ', CheckMaxUnitsViewModel);
                if (initialBind) {
                    ko.applyBindings({ viewModel: CheckMaxUnitsViewModel }, document.getElementById("divStudentListContent"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            });
        };

       

        
    }

    return CheckMaxUnitsViewModel;
}
);
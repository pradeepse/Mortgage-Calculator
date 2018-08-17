function fnConverDate(input) {
    if (typeof input !== "object") return input;
    for (var key in input) {
        if (!input.hasOwnProperty(key)) continue;
        var value = input[key];
        var type = typeof value;
        var match;
        if (value.EffectiveStartDate)
        {
            var parsedDate = new Date(parseInt(value.EffectiveStartDate.substr(6)));
            value.EffectiveStartDate = new Date(parsedDate);
        }
        if (value.EffectiveEndDate) {
            var parsedEffectiveEndDate = new Date(parseInt(value.EffectiveEndDate.substr(6)));
            value.EffectiveEndDate = new Date(parsedEffectiveEndDate);
        }
        else if (type === "object") {
            fnConverDate(value);
        }
    }
}

var mortgageapp = angular.module('mortgageapp', ['ngRoute', 'ui.grid', 'ui.grid.selection']);

mortgageapp.service("mortgageservice", function ($http) {
    //Function to call get genre web api call  
    this.GetEmployee = function () {
        var req = $http.get('/Mortgage/GetMortgageDetails');
        return req;
    }
});

mortgageapp.controller("mortgagecontroller", function ($scope, mortgageservice, $filter, $window) {
    GetEmployee();

    function GetEmployee(uiGridConstants) {
        
        mortgageservice.GetEmployee().then(function (result) {
            fnConverDate(result.data);
            $scope.Employees = result.data;
            console.log($scope.Employees);
        }, function (error) {
            $window.alert('Oops! Something went wrong while fetching genre data.');
        })
    }
    //Used to bind ui-grid    
    //$scope.selectedItem = null;
    $scope.gridOptions = {
        enableRowSelection: true,
        //paginationPageSizes: [5, 10, 20, 30, 40],
        //paginationPageSize: 10,
        enableSorting: true,
        enableHiding: false,
        multiSelect: false,

        onRegisterApi: function (gridApi) {  
            $scope.gridApi = gridApi;  
            $scope.MortgageId = undefined;
            $scope.Name = undefined;
            $scope.MortgageType = undefined;
            $scope.InterestRepayment = undefined;
            $scope.CancellationFee = undefined;
            $scope.EstablishmentFee = undefined;
            gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                if (row.isSelected) {
                    $scope.showTable = true;
                $scope.MortgageId = row.entity.MortgageId;
                $scope.Name = row.entity.Name;
                $scope.MortgageType = row.entity.MortgageType;
                $scope.InterestRepayment = row.entity.InterestRepayment;
                $scope.CancellationFee = row.entity.CancellationFee;
                $scope.EstablishmentFee = row.entity.EstablishmentFee;
                }
            }); 
        },

        columnDefs: [{
            name: 'MortgageId',
            displayName: 'ID',
            width: '8%'
        }, {
            name: 'Name',
            field: 'Name',
            headerCellClass: 'tablesorter-header-inner',
            width: '15%'
        }, {
            name: 'MortgageType',
            field: 'MortgageType',
            displayName: 'Type',
            headerCellClass: 'tablesorter-header-inner'
        }, {
            name: 'InterestRepayment',
            field: 'InterestRepayment',
            displayName: 'Interest',
            headerCellClass: 'tablesorter-header-inner'
        }, {
            name: 'EffectiveStartDate',
            field: 'EffectiveStartDate',
            displayName: 'Start Date',
            headerCellClass: 'tablesorter-header-inner',
            type:'date',
            cellFilter: 'date:\'MM/dd/yyyy\''
        }, {
            name: 'EffectiveEndDate',
            field: 'EffectiveEndDate',
            displayName: 'End Date',
            headerCellClass: 'tablesorter-header-inner',
            type:'date',
            cellFilter: 'date:\'MM/dd/yyyy\''
        }, {
            name: 'TermsInMonths',
            field: 'TermsInMonths',
            displayName: 'Term(Month)',
            headerCellClass: 'tablesorter-header-inner',
            enableSorting: false,
        }, {
            name: 'CancellationFee',
            field: 'CancellationFee',
            displayName: 'Cancel Fee',
            headerCellClass: 'tablesorter-header-inner',
        }, {
            name: 'EstablishmentFee',
            field: 'EstablishmentFee',
            displayName: 'Est Fee',
            headerCellClass: 'tablesorter-header-inner',
        }, {
            name: 'SchemaIdentifier',
            field: 'SchemaIdentifier',
            displayName: 'Schema',
            headerCellClass: 'tablesorter-header-inner',
        }

        ],
        data: 'Employees'
        
    };

   
});
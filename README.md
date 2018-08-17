# MortgageCalculator
The Mortgage Calculator application is used to provide right loan plan and provide details about interest and repayment of loan.This is web application which can be easily accessed to perform mortgage calculation  and provide mortgages which are present to the end user. 

Softwares used for this application: 

* Visual Studio 2017 

* MVC 5 

* Web API 2 

* Angular JS 

* MsTest 

* MOQ 

Formulas

The formula used to obtain results are:
  
  Monthly Payment Formula

    M = P r(1+r)^n/(1+r)^n-1
    where P is Loan Amount
          r is Interest rate of month
          n is duration (no.of years * 12)
  
 Total Repayment Formula
    
      T = M * n
      where M is Monthly Payment
            n is duration
       
  Total Interest Paid Formula
  
      I = T - P
      where T is Total Repayment
            P is Loan Amount
            
            
Please find below details of application

Home Page

The Mortgage Calculator application is launched with Home Page which is used to contain menu to access mortgage calculation and to view active mortgages present.

Mortgage Calculator 

The user clicks on ‘View details’ link of Mortgage Calculator to access Mortgage Calculator page. 

The user needs to provide input details like Loan Amount, Loan Duration Period (in terms of Years) and Interest Rate (in terms of Year).Once user submits the details based on the inputs calculator is used to perform calculation and provide output of Total repayment over the lifetime of the loan, Amount paid in interest over the life of the loan.

In Mortgage Calculator, the input fields are mandatory to be filled by user and validation has been done using MVC Model Validation.

Active Mortgages 

The user clicks on ‘View Details’ link on Active Mortgages to access Active Mortgages page. 

The Active Mortgages details are displayed in the grid used to display details of Mortgages like Mortgage ID, Name, Mortgage Type, Interest Rate, Effective Start Date, Effective End Date, Term(in terms of Months), Cancellation Fee, Establishment Fee and Schema Identifier.The grid is provided with features like Sorting, Selection and Hiding.The user can able to view all active mortgage details and perform operations.

In Active Mortgages, Angular ui-grid was implemented.
The grid has features like Sorting, Filtering, Grouping, Column Resizing and user interaction.
Performs well with large data sets.Even 10,000+ rows

Active Mortgages Sorting 

The Active Mortgages grid is used to perform Sorting for multiple columns.The result will be provided with order of sorting with numeric position.

Active Mortgages Selection: 

The Active Mortgage grid is used to perform selection of row and selected row will be retrieved individually.User have to select row by selecting on that row.Please find the screenshot given below.



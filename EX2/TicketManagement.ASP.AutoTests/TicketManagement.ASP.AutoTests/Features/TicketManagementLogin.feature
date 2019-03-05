Feature: TrainingByLogin
	As a user
	I want to be able to login site
	So I can do it from login form

Scenario: Login is succeeded with credentials
	Given User is on login page
	When Enters "admin" to user name input
	And Enters "adminboss" to password field
	And Clicks Sign In button on login form
	Then Login form has error "Hello admin!"

Scenario: Login is failed with wrong credentials
	Given User is on login page
	When Enters "asd" to user name input
	And Enters "asd" to password field
	And Clicks Sign In button on login form
	Then Login form has error "Wrong login or password"
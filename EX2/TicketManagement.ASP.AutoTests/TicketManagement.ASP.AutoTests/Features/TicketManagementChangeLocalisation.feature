Feature: TrainingByChangeLocalisation
	As a user
	I want to be able to change site language
	So I can do it through home page header

Scenario: Set localisation to English
	Given User is on login page without language preparing
	When User sets site language to "en-us"
	Then Banner text is "Hello admin!"
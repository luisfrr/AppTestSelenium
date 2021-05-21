Feature: Examples
  Optional description of feature

  @test
  Scenario: Register an existing user
    Given I navigate to 'Register' page
    When I set element 'Email' with text 'luisfrr27@gmail.com'
    And I set element 'Password' with text 'cotemar'
    And I set element 'Confirm Password' with text 'cotemar'
    And I click in element 'Register Button'
    Then Assert if element 'Register Form Error' contains text 'Username 'luisfrr27@gmail.com' is already taken.'

  @test
  Scenario: Login with correct credentials
    Given I navigate to 'Login' page
    When I set element 'Email' with text 'luisfrr27@gmail.com'
    And I set element 'Password' with text 'cotemar'
    And I click in element 'Login Button'
    Then Assert if title page is equals to 'Home page - SimpleWebApp'
    And Assert if element 'Profile' text is equals to 'Hello luisfrr27@gmail.com!'

  @test
  Scenario: Login with incorrect credentials
    Given I navigate to 'Login' page
    When I set element 'Email' with text 'luisfrr27@gmail.com'
    And I set element 'Password' with text 'cotemar123'
    And I click in element 'Login Button'
    Then Assert if element 'Login Form Error' contains text 'Invalid login attempt.'

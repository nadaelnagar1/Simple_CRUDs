import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { toast, ToastContainer } from 'react-toastify';
import { Modal, Button, Form } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-toastify/dist/ReactToastify.css';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = {
      students: [],
      showAddModal: false,
      showEditModal: false,
      newStudentData: {
        firstName: '',
        lastName: '',
        emailAddress: '',
        birthDate: '',
        gender: '',
        gpa: '',
        studentImage: '',
      },
      editStudentData: {
        firstName: '',
        lastName: '',
        emailAddress: '',
        gpa: 0,
        studentImage: ''
      },
      editStudentId: null 
    };
  }

  
  handleEditModal = (studentId) => {
    // Fetch student data by ID
    fetch(`https://localhost:7109/Student/GetStudentById/${studentId}`)
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to fetch student data');
        }
        return response.json();
      })
      .then(data => {
        this.setState({
          showEditModal: true,
          editStudentData: {
            firstName: data.firstName,
            lastName: data.lastName,
            emailAddress: data.emailAddress,
            gpa: data.gpa,
            studentImage: data.studentImage
          },
          editStudentId: studentId
        });

      })
      .catch(error => {

        console.error('Error:', error);
        // Handle errors here
        toast.error('Failed to fetch student data');
      });
  };

  handleEditInputChange = (e) => {
    const { name, value } = e.target;

    let parsedValue = value;
  
    if (name === 'gpa') {
      parsedValue = parseInt(value, 10);
    }
  
    this.setState((prevState) => ({
      editStudentData: {
        ...prevState.editStudentData,
        [name]: parsedValue,
      },
    }));
  };

  handleUpdateStudent = () => {
    const { editStudentData, editStudentId } = this.state;
    fetch(`https://localhost:7109/Student/UpdateStudentById/${editStudentId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(editStudentData),
    })
      .then((response) => {
        if (!response.ok) {
          toast.error('Failed to update student');
          throw new Error('Failed to update student');
        }
        return response.json();
      })
      .then((updatedStudent) => {
        // Update the state with the updated student data
        this.setState((prevState) => ({
          students: prevState.students.map(student =>
            student.id === editStudentId ? updatedStudent : student
          ),
          showEditModal: false,
        }));

        toast.success('Student Updated Successfully');

      })
      .catch((error) => {
        toast.error('Failed to update student');
      });
  };
  handleAddModal = () => {
    this.setState({ showAddModal: !this.state.showAddModal });
  };

  handleInputChange = (e) => {
    const { name, value } = e.target;
  
    let parsedValue = value;
  
    if (name === 'gpa') {
      parsedValue = parseFloat(value); 
    } else if (name === 'gender') {
      parsedValue = value === 'male' ? parseInt(1) : parseInt(2); 
    }
  
    this.setState((prevState) => ({
      newStudentData: {
        ...prevState.newStudentData,
        [name]: parsedValue,
      },
    }));
  };
  

  handleAddStudent = () => {
    // Format birthDate as an ISO string
    const formattedBirthDate = new Date(this.state.newStudentData.birthDate).toISOString();
  
    const requestBody = {
      ...this.state.newStudentData,
      birthDate: formattedBirthDate, // Assign formatted birthDate
    };
  
    fetch('https://localhost:7109/Student/AddStudent', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(requestBody),
    })
      .then((response) => {
        if (!response.ok) {
          toast.error('Failed to add student');
          throw new Error('Failed to add student');
        }
        return response.json(); // assuming the response contains the added student data
      })
      .then((addedStudent) => {
        this.setState((prevState) => ({
          students: [...prevState.students, addedStudent], // Add the newly added student to the existing array
          showAddModal: false, // Hide the modal
        }));
        toast.success('Student Added Successfully');
      })
      .catch((error) => {
        toast.error('Failed to add student');
      });
  };
  
  formatDate = (dateString) => {
    const date = new Date(dateString);
    const day = date.getDate();
    const month = date.getMonth() + 1; // Month is zero-indexed
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
  }

  componentDidMount() {
    // Fetch all students
    fetch('https://localhost:7109/Student/GetAllStudents') 
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then(data => {
              this.setState({ students: data });

      })
      .catch(error => {
        console.error('Error:', error);
        // Handle errors here
      });
  }

  handleDelete = (studentId) => {
    const confirmDelete = window.confirm('Are you sure you want to delete this student?');

    if (confirmDelete) {
    fetch(`https://localhost:7109/Student/DeleteStudent/${studentId}`, {
      method: 'DELETE',
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to delete student');
        }
        // Assuming successful deletion
        // Remove the deleted student from the state (assuming the API returns success)
        this.setState(prevState => ({
          students: prevState.students.filter(student => student.id !== studentId),
        }));
        toast.success('Deleted Successfully');
      })
      .catch(error => {
        console.error('Error:', error);
        // Handle errors here
        toast.error('Deletion failed');

      });}
  };

  render() {
    const { students, showAddModal, newStudentData,
      editStudentData,showEditModal } = this.state;

    return (
      <div>
        <div className="d-grid gap-2 d-md-flex justify-content-md-end">
        <button type="button" className="btn btn-primary mr-2" onClick={this.handleAddModal}>Add Student</button>
        </div>
        <Modal show={showAddModal} onHide={this.handleAddModal}>
          <Modal.Header closeButton>
            <Modal.Title>Add Student</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
              <Form.Group controlId="firstName">
                <Form.Label>First Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter first name"
                  name="firstName"
                  value={newStudentData.firstName}
                  onChange={this.handleInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="lastName">
                <Form.Label>Last Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter last name"
                  name="lastName"
                  value={newStudentData.lastName}
                  onChange={this.handleInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="email">
                <Form.Label>Email Address</Form.Label>
                <Form.Control
                  type="email"
                  placeholder="Enter email"
                  name="emailAddress"
                  value={newStudentData.emailAddress}
                  onChange={this.handleInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="birthDate">
                <Form.Label>Birth Date</Form.Label>
                <Form.Control
                  type="date"
                  name="birthDate"
                  value={newStudentData.birthDate}
                  onChange={this.handleInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="gender">
                <Form.Label>Gender</Form.Label>
                <Form.Control
                  as="select"
                  name="gender"
                  value={newStudentData.gender}
                  onChange={this.handleInputChange}
                  required 
                >
                  <option value="male">Male</option>
                  <option value="female">Female</option>
                </Form.Control>
              </Form.Group>

              <Form.Group controlId="gpa">
                <Form.Label>GPA</Form.Label>
                <Form.Control
                  type="number"
                  step="0.01"
                  placeholder="Enter GPA"
                  name="gpa"
                  value={newStudentData.gpa}
                  onChange={this.handleInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="studentImage">
                <Form.Label>Student Image</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter image URL"
                  name="studentImage"
                  value={newStudentData.studentImage}
                  onChange={this.handleInputChange}
                  required 
                />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={this.handleAddModal}>
              Cancel
            </Button>
            <Button variant="primary" onClick={this.handleAddStudent}>
              Add
            </Button>
          </Modal.Footer>
        </Modal>

<Modal show={showEditModal} onHide={() => this.setState({ showEditModal: false })}>
          <Modal.Header closeButton>
            <Modal.Title>Edit Student</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
              <Form.Group controlId="editFirstName">
                <Form.Label>First Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter first name"
                  name="firstName"
                  value={editStudentData.firstName}
                  onChange={this.handleEditInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="editLastName">
                <Form.Label>Last Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter last name"
                  name="lastName"
                  value={editStudentData.lastName}
                  onChange={this.handleEditInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="editEmail">
                <Form.Label>Email Address</Form.Label>
                <Form.Control
                  type="email"
                  placeholder="Enter email"
                  name="emailAddress"
                  value={editStudentData.emailAddress}
                  onChange={this.handleEditInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="editGPA">
                <Form.Label>GPA</Form.Label>
                <Form.Control
                  type="number"
                  step="0.01"
                  placeholder="Enter GPA"
                  name="gpa"
                  value={editStudentData.gpa}
                  onChange={this.handleEditInputChange}
                  required 
                />
              </Form.Group>

              <Form.Group controlId="editStudentImage">
                <Form.Label>Student Image</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter image URL"
                  name="studentImage"
                  value={editStudentData.studentImage}
                  onChange={this.handleEditInputChange}
                  required 
                />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => this.setState({ showEditModal: false })}>
              Cancel
            </Button>
            <Button variant="primary" onClick={this.handleUpdateStudent}>
              Update
            </Button>
          </Modal.Footer>
        </Modal>

        {students.length > 0 ? (
          <table className="table table-sm">
            <thead>
              <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Birthdate</th>
                <th>Gender</th>
                <th>GPA</th>
                <th>Actions</th>

              </tr>
            </thead>
            <tbody>
              {students.map(student => (
                 <tr key={student.id}>
                 <td>{student.firstName} {student.lastName}</td>
                 <td>{student.emailAddress}</td>
                 <td>{this.formatDate(student.birthDate)}</td>
                 <td>{student.gender==1?'Male':'Female'}</td>
                 <td>{student.gpa}</td>
                 <td>
                   <button type="button" className="btn btn-success m-2" onClick={() => this.handleEditModal(student.id)} >Edit</button>
                   <button type="button" className="btn btn-danger m-2" onClick={() => this.handleDelete(student.id)}>Delete</button>
                 </td>
               </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p>No content to be shown</p>
        )}

      </div>
    );
  }
}

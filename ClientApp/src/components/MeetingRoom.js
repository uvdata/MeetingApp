import React, { Component, createRef } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Map, TileLayer, Marker, Popup, FeatureGroup} from 'react-leaflet';
import L from 'leaflet';
import icon from 'leaflet/dist/images/marker-icon.png';
import iconShadow from 'leaflet/dist/images/marker-shadow.png';
import 'leaflet/dist/leaflet.css'

import { Button, Modal, Form, Row, Col, Table } from 'react-bootstrap';

let DefaultIcon = L.icon({
    iconUrl: icon,
    shadowUrl: iconShadow,
    iconSize: [25, 41],
    iconAnchor: [12.5, 41],
    popupAnchor: [0, -41]
});

L.Marker.prototype.options.icon = DefaultIcon;

let urlDev = "http://localhost:9000"


class MeetingRoomModel extends Component{
    constructor(props){
        super(props)
        
        let defaultValues = {
            id: null,
            name: '',
            description: '',
            location: '',
            totalSeats: 0,
            marker:null
        }

        if(props.meetingRoom){
            defaultValues = props.meetingRoom;
            if(props.meetingRoom.location && props.meetingRoom.location.coordinates){
                defaultValues.marker = props.meetingRoom.location.coordinates;
            }
        }

        this.state = {
            id: defaultValues.id,
            name: defaultValues.name,
            description: defaultValues.description,
            location: defaultValues.location,
            totalSeats: defaultValues.totalSeats,
            marker: defaultValues.marker
        }

        this.mapRef = createRef();
    }


    handleChange = e => {
        console.log(e.target.value)
        if(e.target.id == "totalSeats" && e.target.value < 0 || e.target.id == "totalSeats" && e.target.value == NaN){
            return false;
        }
        let key = e.target.id;
        let value = () => {
            if(key.toLowerCase() == "totalseats"){
                return parseInt(e.target.value);
            }else{
                return e.target.value;
            }
        }
        this.setState({[key]: value()}, () => {
            if(this.props.updateValues){
                this.props.updateValues(this.state);
            }
        });
    }

    handleMapClick = (e) => {
        console.log(e);
        this.setState({marker: [e.latlng.lat, e.latlng.lng]});
        if(this.props.updateValues){
            this.props.updateValues(this.state);
        }
    }

    render(){
        let marker = null;

        if(this.state.marker != null){
            marker = <Marker position={this.state.marker}></Marker>
            console.log(marker);
        }

        return <React.Fragment>
            <Form>            
                <Form.Group as={Row} controlId="name">
                    <Form.Label column sm={3}>Room Name: </Form.Label>
                    <Col><Form.Control value={this.state.name} placeholder="Name" onChange={this.handleChange} /></Col>
                </Form.Group>

                <Form.Group as={Row} controlId="description">
                    <Form.Label column sm={3}>Room Description: </Form.Label>
                    <Col><Form.Control value={this.state.description} placeholder="Description" onChange={this.handleChange} /></Col>
                </Form.Group>

                <Form.Group as={Row} controlId="location">
                    <Form.Label column sm={3}>Room Location: </Form.Label>
                    {/* <Col><Form.Control value={this.state.location} placeholder="Location" onChange={this.handleChange} /></Col> */}
                    <Col>
                        <Map ref={this.mapRef} center={{lat: 51.505, lng: -0.09}} zoom={5} style={{width:"100%", height: "600px"}} onclick={this.handleMapClick} >
                        {/* <TileLayer
                        attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        /> */}
                        {marker != null ? marker: ''}
                        </Map>
                    </Col>
                </Form.Group>

                <Form.Group as={Row} controlId="totalSeats">
                    <Form.Label column sm={3}>Total seats: </Form.Label>
                    <Col><Form.Control type="number" min="0" value={this.state.totalSeats} onChange={this.handleChange} /></Col>
                </Form.Group>
            </Form>
        </React.Fragment>
    }
}


class AddMeetingRoom extends Component{
    constructor(props){
        super(props)
        this.state = {
            show: false,
            values: null
        }
    }

    handleShow = () => {
        this.setState({show:true})
    }

    handleClose = () => {
        this.setState({show:false})
    }

    async postMeetingRooms(values) {
        let method = 'POST';
        let id = '';
        if(values.id){
            method = 'PUT';
            id = '/'+values.id;
        }

        if(values.marker){
            values.location = {
                "type": "Point",
                "coordinates": [
                    values.marker[0],
                    values.marker[1]
                ]
            }
        }
        delete values.marker;
        
        if(values.id == null){
            delete values.id;
        }
        
        //console.log('put values',values);

        async function postData(url = '', data = {}) {
            // Default options are marked with *
            const response = await fetch(url, {
                method: method, // *GET, POST, PUT, DELETE, etc.
                cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
                credentials: 'same-origin', // include, *same-origin, omit
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data) // body data type must match "Content-Type" header
            });
            if(method == 'PUT'){
                return response;
            }
            else{
                return response.json(); // parses JSON response into native JavaScript objects
            }
        }

        return postData('/api/MeetingRooms'+id, { ...values })
        .then(data => {
            this.setState({show:false});
            console.log(data); // JSON data parsed by `data.json()` call
            this.props.updateData();
        }).catch(error => {
            alert("Something went wrong! Error:" + JSON.stringify(error));
        });
    }

    handleSubmit = () => {
        
        this.postMeetingRooms(this.state.values);
    }

    updateValues = (values) => {
        //console.log('values', values);
        this.setState({values: values});
    }

    render(){
        //Override button text for edit 
        let buttonText = "+ Add meeting room";
        if(this.props.mode == "edit"){
            buttonText = "Edit";
        }

        //
        let item = null;
        if(this.props.item){
            item = this.props.item
        }
        

        return (
            <React.Fragment>
                <Button variant="primary" onClick={this.handleShow}>
                    {buttonText}
                </Button>

                <Modal size="lg" show={this.state.show} onHide={this.handleClose}>
                    <Modal.Header closeButton>
                    <Modal.Title>Add meeting</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <MeetingRoomModel meetingRoom={item} updateValues={this.updateValues}></MeetingRoomModel>
                    </Modal.Body>
                    <Modal.Footer>
                    <Button variant="secondary" onClick={this.handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={this.handleSubmit}>
                        Save Changes
                    </Button>
                    </Modal.Footer>
                </Modal>
            </React.Fragment>
        )
    }
}


class DeleteDialog extends Component{
    constructor(props){
        super(props)

        this.state = {
            id: this.props.id,
            show: false
        }
    }

    handleShow = () => {
        this.setState({show:true})
    }

    handleClose = () => {
        this.setState({show:false})
    }

    handleSubmit = () => {
        this.props.confirmDelete(this.state.id);
    }

    render(){
        return <React.Fragment>
                <Button variant="danger" onClick={this.handleShow}>
                    Remove
                </Button>

                <Modal size="lg" show={this.state.show} onHide={this.handleClose}>
                    <Modal.Header closeButton>
                    <Modal.Title>Remove Meeting Room?</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        Would you like to delete this meeting room?
                    </Modal.Body>
                    <Modal.Footer>
                    <Button variant="secondary" onClick={this.handleClose}>
                        Close
                    </Button>
                    <Button variant="danger" onClick={this.handleSubmit}>
                        Confirm Delete
                    </Button>
                    </Modal.Footer>
                </Modal>
        </React.Fragment>
    }
}

class MeetingRoomTable extends Component{
    constructor(props){
        super(props)
        this.state = {
            
        }
        //console.log(props);
    }

    confirmDelete = (id) => {
        this.props.deleteMeetingRoom(id);        
    }

    hightlight = (id) => {
        this.props.highlight(id);
    }   

    render(){
        let items = null
        if(this.props.items){
            items = this.props.items.map(item => {
                return (
                    <tr key={item.id}>
                        <td>{item.name}</td>
                        <td>{item.description}</td>
                        <td><Button variant="info" onClick={() => {this.props.highlight(item.id)}}>Show Location</Button></td>
                        <td>{item.totalSeats}</td>
                        <td> <DeleteDialog id={item.id}  confirmDelete={this.confirmDelete} /> <AddMeetingRoom mode="edit" updateData={this.props.updateData} item={item} /></td>
                    </tr>
                )
            })
        }

        return(
            <React.Fragment>
                <Table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                            <th>Location</th>
                            <th>Total Seats</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {items}
                    </tbody>
                </Table>
            </React.Fragment>
        )
    }
}

class MeetingRoomComponent extends Component{
    constructor(props){
        super(props)
        this.state = {
            lat: 51.505,
            lng: -0.09,
            zoom: 13,
            meetingRooms: null
        }       
        this.mapRef = createRef();
        this.layerGroupRef = createRef();
        window.layerGroupRef = this.layerGroupRef;

        this.markerRefs = [];
        window.markerRefs = this.markerRefs;
    }

    async getMeetingRooms(){
        //console.log('getting information!');
        const response = await fetch('/api/meetingrooms');
        const jsonResponse = await response.json();
        //console.log(jsonResponse);
        this.setState({meetingRooms: jsonResponse});
        
    }

    componentDidMount(){
        
        window.getMeetingRooms = this.getMeetingRooms.bind(this);
        this.getMeetingRooms().then(() => {
            if(this.mapRef.current && this.layerGroupRef.current){
                this.mapRef.current.leafletElement.fitBounds(this.layerGroupRef.current.leafletElement.getBounds());
                this.mapRef.current.leafletElement.setZoom(this.mapRef.current.leafletElement.getZoom()-1);
            }
        })
    }

    updateData = () => {
        this.markerRefs = [];
        this.getMeetingRooms();
    }

    async deleteMeetingRoom(id){
        async function postData(url = '', data = {}) {
            const response = await fetch(url, {
                method: 'DELETE',
                body: JSON.stringify(data)
            });
            return;
        }

        return postData('/api/meetingrooms/'+id)
        .then(() => {
            this.updateData();
        });
    }

    async updateMeetingRoom(data){
        async function postData(url = '', data = {}) {
            const response = await fetch(url, {
                method: 'PUT',
                body: JSON.stringify(data)
            });
            return;
        }

        return postData('/api/meetingrooms/'+data.id, data)
        .then(() => {
            this.updateData();
        });
    }

    highlight = (id) => {
        console.log('highlight!!', this.markerRefs[id]);
        /*if(this.layerGroupRef.current.props.children.length > 0){
            let resultMeetingRoom = this.layerGroupRef.current.props.children.filter(a => {return a.key == id})[0];
            console.log(resultMeetingRoom);
            //this.mapRef.current.leafletElement.fitBounds(resultMeetingRoom.getBounds());
        }*/
        this.markerRefs[id].leafletElement.openPopup();
    }

    render(){
        const position = [this.state.lat, this.state.lng]

        let MarkerGroup = () => {
            if(this.state.meetingRooms){
                return this.state.meetingRooms.filter((e, i) => {
                    if(e.location != null && e.location.coordinates != null){
                        return true;
                    }
                    return false;
                }).map((e, i) => {
                    let container = null;
                    if(e.location != null && e.location.coordinates != null){
                        container = <Marker position={e.location.coordinates} key={e.id} ref={ref => {this.markerRefs[e.id] = ref}} highlight={() => {console.log('im a marker',this)}} onclick={() => {console.log(this)}}><Popup>Here I  am!</Popup> </Marker> 
                    }
                    return container;
                });
            }else{
                return null;
            }
        }


        return (
            <React.Fragment>
                <AddMeetingRoom updateData={this.updateData}></AddMeetingRoom>
                
                <Map ref={this.mapRef} center={position} zoom={this.state.zoom} style={{width:"100%", height: "600px"}}  >
                    {/* <TileLayer
                    attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    /> */}
                    <FeatureGroup ref={this.layerGroupRef} onlayeradd={this.fitToCustomLayer}>
                        {this.state.meetingRooms ? MarkerGroup(): ''}
                    </FeatureGroup>
                    
                </Map>
                
                
                <MeetingRoomTable items={this.state.meetingRooms} updateData={this.updateData} deleteMeetingRoom={this.deleteMeetingRoom} highlight={this.highlight.bind(this)}></MeetingRoomTable>
            </React.Fragment>
        )
    }
}

export default MeetingRoomComponent;
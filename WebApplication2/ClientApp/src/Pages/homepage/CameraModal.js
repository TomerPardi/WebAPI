import React from "react";
import { useState, useContext } from "react";
import { Button, Modal } from "react-bootstrap";
import AppContext from "../../AppContext";

export const CameraModal = (props) => {
  const sharedContext = useContext(AppContext);
  const [show, setShow] = useState(props.show);
  const [photo, setPhoto] = useState(null);

  const handleClose = (stream) => {
    setPhoto(null);
    setShow(false);
    stream.getTracks().forEach(function (track) {
      track.stop();
    });
    // clean the saved stream in the context
    sharedContext.stream = null;
  };

  const handleShow = () => setShow(true);

  function helperForCam(stream) {
    sharedContext.stream = stream;
    var canvas = document.getElementById("canv");
    var button = document.getElementById("photoButt");
    var button2 = document.getElementById("cancelButt");
    var button3 = document.getElementById("sendButt");
    var video = document.querySelector("#videoElement");
    if (video == null) {
      stream.getTracks().forEach(function (track) {
        track.stop();
        stream.removeTrack(track);
      });
      return;
    }
    video.srcObject = stream;
    video.src = stream;
    var w = stream.videoWidth;
    var h = stream.videoHeight;
    video.addEventListener("loadedmetadata", function () {
      w = this.videoWidth;
      h = this.videoHeight;
      canvas.width = w;
      canvas.height = h;
    });

    // take photo button
    button.onclick = function () {
      canvas.getContext("2d").drawImage(video, 0, 0, w, h, 0, 0, w, h);
      var img = canvas.toDataURL("image/png");
      setPhoto(img);
      button3.disabled = false;
    };

    // cancel button
    button2.onclick = function () {
      handleClose(stream);
    };

    //send button
    button3.onclick = function () {
      props.handler(photo);
      handleClose(stream);
    };
  }

  function cam() {
    navigator.getUserMedia =
      navigator.getUserMedia ||
      navigator.webkitGetUserMedia ||
      navigator.mozGetUserMedia ||
      navigator.msGetUserMedia;
    if (sharedContext.stream == null) {
      navigator.getUserMedia(
        { video: true },
        function (stream) {
          helperForCam(stream);
        },
        function (err) {
          alert("there was an error " + err);
        }
      );
    } else {
      helperForCam(sharedContext.stream);
    }
  }

  return (
    <>
      {/* <Button variant="primary" onClick={handleShow} style={{ display: 'none' }}>
            </Button> */}

      <Modal
        show={show}
        onHide={handleClose}
        backdrop='static'
        keyboard={false}
      >
        <Modal.Header>
          <Modal.Title>Camera Mode</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className='container'>
            <canvas
              id='canv'
              style={{ width: "500", height: "500", display: "none" }}
            ></canvas>
            {
              <video
                className='col'
                autoplay='true'
                id='videoElement'
                style={{
                  width: "100%",
                  visibility: photo ? "hidden" : "visible",
                  display: photo ? "none" : "flex",
                }}
              ></video>
            }

            {photo && <img src={photo} style={{ width: "100%" }}></img>}

            {show && cam()}
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button id='cancelButt' variant='secondary'>
            Cancel
          </Button>
          <Button id='photoButt' variant='primary'>
            Take photo
          </Button>
          <Button id='sendButt' variant='primary' disabled={true}>
            Send
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default CameraModal;

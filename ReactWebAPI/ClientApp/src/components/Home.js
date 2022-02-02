import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Hello, world!</h1>
            <>
                <title />
                <meta charSet="utf-8" />
                <meta name="viewport" content="width=device-width, initial-scale=1" />
                <link rel="stylesheet" href="css/bootstrap5.1.3/bootstrap.css" />
                <link rel="stylesheet" href="css/bootstrap5.1.3/bootstrap.min.css" />
                <link rel="stylesheet" href="css/style.css" />
                <div className="container-fluid p-0">
                    <div className="row m-0 loginPanel">
                        <div className="col-xl-5 col-12 loginLeftPanel p-0 text-center">
                            <div className="row m-0">
                                <div className="col-12 p-0 mt-4">
                                    <img src="images/logo.svg" className="img-fluid" />
                                </div>
                                <div className="col-12 p-0 mt-3">
                                    <img src="images/login_illustration.svg" className="img-fluid" />
                                </div>
                            </div>
                            <div className="row m-0">
                                <div className="col-xl-12">
                                    <p className="text-white footerTxt">
                                        Corazon 2.0.0 © 2020. Powered by OS HRS
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div className="col-xl-7 col-12">
                            <div className="row m-0 mt-3">
                                <div className="col-xl-12 d-flex justify-content-end">
                                    <div className="form-group ddlLang">
                                        <select className="form-select">
                                            <option value={1}>Eng</option>
                                        </select>
                                    </div>
                                </div>
                                <div className="col-xl-12 text-center mt-5">
                                    <h4 className="fontBold">
                                        Access to Payroll Any Time, Any Place, Anywhere
                                    </h4>
                                    <p className="mt-3 essPrimaryfontbold">
                                        World Class Employee Experience
                                    </p>
                                </div>
                                <div className="col-xl-12 d-flex justify-content-center mt-4">
                                    <div className="form-group">
                                        <label htmlFor="txtUsrname">
                                            User Name or Email Address<span className="text-danger">*</span>
                                        </label>
                                        <input className="form-control" id="txtUsrname" type="text" />
                                    </div>
                                </div>
                                <div className="col-xl-12 mt-3 d-flex justify-content-center">
                                    <div className="form-group">
                                        <label htmlFor="txtPasswd">
                                            Password<span className="text-danger">*</span>
                                        </label>
                                        <input className="form-control" id="txtPasswd" type="password" />
                                    </div>
                                </div>
                                <div className="col-xl-10 d-flex justify-content-end">
                                    <a href="#" className="hypLink pe-4">
                                        Forgot Password?
                                    </a>
                                </div>
                                <div className="col-xl-12 mt-3 d-flex justify-content-center">
                                    <button type="button" className="btn btnEssPrimary mx-2">
                                        Login
                                    </button>
                                    <button type="button" className="btn btnEssSecondary mx-2">
                                        Cancel
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </>

        </div>
    );
  }
}

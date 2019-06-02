/*---------------------------------------------------------------------------------------------
*  Copyright (c) Microsoft Corporation. All rights reserved.
*  Licensed under the MIT License. See License.txt in the project root for license information.
*--------------------------------------------------------------------------------------------*/

import * as vscode from 'vscode';
import { ext } from '../extensionVariables';
import { localize } from '../localize';

// tslint:disable-next-line:export-name
export async function writeToEditor(editor: vscode.TextEditor, data: string): Promise<void> {
    await editor.edit((editBuilder: vscode.TextEditorEdit) => {
        if (editor.document.lineCount > 0) {
            const lastLine: vscode.TextLine = editor.document.lineAt(editor.document.lineCount - 1);
            editBuilder.delete(new vscode.Range(new vscode.Position(0, 0), new vscode.Position(lastLine.range.start.line, lastLine.range.end.character)));
        }
        editBuilder.insert(new vscode.Position(0, 0), data);
    });
}

export function addFolderToWorkspaceIfNotExists(folderPath: string): void {
    let folderInWorkspace: boolean = true;
    if (vscode.workspace.workspaceFolders !== undefined
        && vscode.workspace.workspaceFolders.length > 0) {
        const folder = vscode.workspace.workspaceFolders.find((w) => w.uri.fsPath === folderPath);
        if (!folder) {
            folderInWorkspace = false;
        }
    } else {
        folderInWorkspace = false;
    }

    if (!folderInWorkspace) {
        const message: string = localize('openFolderInWorkspace', 'For better editing experience open folder "{0}".', folderPath);
        const btn: vscode.MessageItem = { title: localize('openFolder', 'Open Folder') };
        // tslint:disable-next-line: no-floating-promises
        ext.ui.showWarningMessage(message, btn).then(async result => {
            if (result === btn) {
                vscode.commands.executeCommand("vscode.openFolder", vscode.Uri.file(folderPath));
            }
        });
    }
}